using POSInventorySystem.Data;
using POSInventorySystem.Models;

namespace POSInventorySystem.Services;

public class InventoryService
{
    public void DisplayAllProducts()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("         PRODUCT INVENTORY");
        Console.WriteLine("=================================");

        foreach (Product product in DataStore.Products)
        {
            product.DisplayDetails();
            Console.WriteLine();
        }
    }

    public Product? FindProduct(string productId)
    {
        return DataStore.Products
            .FirstOrDefault(product => product.ProductId.Equals(
                productId,
                StringComparison.OrdinalIgnoreCase
            ));
    }

    public bool RestockProduct(string productId, int quantity)
    {
        Product? product = FindProduct(productId);

        if (product == null || quantity <= 0)
        {
            return false;
        }

        product.AddStock(quantity);
        return true;
    }

    public bool RemoveStock(string productId, int quantity)
    {
        Product? product = FindProduct(productId);

        if (product == null || quantity <= 0 || product.Stock < quantity)
        {
            return false;
        }

        product.RemoveStock(quantity);
        return true;
    }

    public List<Product> GetLowStockProducts()
    {
        return DataStore.Products
            .Where(product => product.IsLowStock())
            .ToList();
    }
}