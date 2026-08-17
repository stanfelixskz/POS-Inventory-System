using POSInventorySystem.Data;
using POSInventorySystem.Models;

namespace POSInventorySystem.Services;

public class AdminService
{
    public void ViewInventory()
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("        ADMIN INVENTORY");
        Console.WriteLine("=================================");

        foreach (Product product in DataStore.Products)
        {
            Console.WriteLine(
                $"{product.ProductId} | {product.Name} | " +
                $"₱{product.Price:F2} | Stock: {product.Stock}"
            );
        }

        Console.WriteLine("=================================");
    }
    public void AddProduct()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("          ADD PRODUCT");
    Console.WriteLine("=================================");

    Console.Write("Enter Product ID: ");
    string? productId = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(productId))
    {
        Console.WriteLine("Invalid Product ID.");
        return;
    }

    bool idExists = DataStore.Products.Any(
        product => product.ProductId == productId
    );

    if (idExists)
    {
        Console.WriteLine("Product ID already exists.");
        return;
    }

    Console.Write("Enter Product Name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        Console.WriteLine("Invalid product name.");
        return;
    }

    Console.Write("Enter Price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
    {
        Console.WriteLine("Invalid price.");
        return;
    }

    Console.Write("Enter Initial Stock: ");
    if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
    {
        Console.WriteLine("Invalid stock quantity.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Select Product Type:");
    Console.WriteLine("1. Food");
    Console.WriteLine("2. Beverage");
    Console.Write("Choose product type: ");

    string? typeChoice = Console.ReadLine();

    Product product;

    switch (typeChoice)
    {
        case "1":
            product = new FoodProduct(
                productId,
                name,
                price,
                stock,
                "Food"
            );
            break;

        case "2":
            product = new BeverageProduct(
                productId,
                name,
                price,
                stock,
                "Beverage"
            );
            break;

        default:
            Console.WriteLine("Invalid product type.");
            return;
    }

    DataStore.Products.Add(product);

    Console.WriteLine();
    Console.WriteLine("Product added successfully.");
    }
    public void UpdateProduct()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("         UPDATE PRODUCT");
    Console.WriteLine("=================================");

    Console.Write("Enter Product ID: ");
    string? productId = Console.ReadLine();

    Product? product = DataStore.Products.FirstOrDefault(
        item => item.ProductId == productId
    );

    if (product == null)
    {
        Console.WriteLine("Product not found.");
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"Current Name: {product.Name}");
    Console.Write("Enter New Name: ");
    string? newName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(newName))
    {
        Console.WriteLine("Invalid product name.");
        return;
    }

    Console.WriteLine($"Current Price: ₱{product.Price:F2}");
    Console.Write("Enter New Price: ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice <= 0)
    {
        Console.WriteLine("Invalid price.");
        return;
    }

    Console.WriteLine($"Current Category: {product.Category}");
    Console.Write("Enter New Category: ");
    string? newCategory = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(newCategory))
    {
        Console.WriteLine("Invalid category.");
        return;
    }

product.UpdateProductInfo(
    newName,
    newPrice,
    newCategory
);

    Console.WriteLine();
    Console.WriteLine("Product updated successfully.");
    }
}