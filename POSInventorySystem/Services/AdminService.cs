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


    public void RestockProduct()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("         RESTOCK PRODUCT");
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
    Console.WriteLine($"Product: {product.Name}");
    Console.WriteLine($"Current Stock: {product.Stock}");

    Console.Write("Enter quantity to add: ");

    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
    {
        Console.WriteLine("Invalid stock quantity.");
        return;
    }

    product.AddStock(quantity);

    Console.WriteLine();
    Console.WriteLine("Product restocked successfully.");
    Console.WriteLine($"New Stock: {product.Stock}");
    }

    public void ViewLowStock()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("          LOW STOCK PRODUCTS");
    Console.WriteLine("=================================");

    bool hasLowStock = false;

    foreach (Product product in DataStore.Products)
    {
        if (product.IsLowStock())
        {
            Console.WriteLine(
                $"{product.ProductId} | {product.Name} | " +
                $"Stock: {product.Stock}"
            );

            hasLowStock = true;
        }
    }

    if (!hasLowStock)
    {
        Console.WriteLine("No products are currently low on stock.");
    }

    Console.WriteLine("=================================");
    }

public void ViewSalesRecords()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("          SALES RECORDS");
    Console.WriteLine("=================================");

    if (DataStore.Transactions.Count == 0)
    {
        Console.WriteLine("No sales transactions recorded.");
        Console.WriteLine("=================================");
        return;
    }

    foreach (Transaction transaction in DataStore.Transactions)
    {
        Console.WriteLine();
        Console.WriteLine($"Transaction ID: {transaction.TransactionId}");
        Console.WriteLine($"Date: {transaction.Date}");

        Console.WriteLine("Items:");

        foreach (CartItem item in transaction.Items)
        {
            Console.WriteLine(
                $"  {item.Product.Name} x{item.Quantity} = ₱{item.Subtotal:F2}"
            );
        }

        Console.WriteLine($"Total: ₱{transaction.Total:F2}");
        Console.WriteLine(
            $"Payment: {transaction.Payment?.GetType().Name ?? "N/A"}"
        );
    }

    Console.WriteLine();
    Console.WriteLine("=================================");
    }


public void ViewSalesSummary()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("          SALES SUMMARY");
    Console.WriteLine("=================================");

    if (DataStore.Transactions.Count == 0)
    {
        Console.WriteLine("No sales transactions recorded.");
        Console.WriteLine("=================================");
        return;
    }

    int totalTransactions = DataStore.Transactions.Count;

    decimal totalSales = DataStore.Transactions.Sum(
        transaction => transaction.Total
    );

    decimal cashSales = DataStore.Transactions
        .Where(transaction => transaction.Payment is CashPayment)
        .Sum(transaction => transaction.Total);

    decimal gcashSales = DataStore.Transactions
        .Where(transaction => transaction.Payment is GCashPayment)
        .Sum(transaction => transaction.Total);

    decimal cardSales = DataStore.Transactions
        .Where(transaction => transaction.Payment is CardPayment)
        .Sum(transaction => transaction.Total);

    Console.WriteLine($"Total Transactions: {totalTransactions}");
    Console.WriteLine($"Total Sales: ₱{totalSales:F2}");
    Console.WriteLine();

    Console.WriteLine($"Cash Sales: ₱{cashSales:F2}");
    Console.WriteLine($"GCash Sales: ₱{gcashSales:F2}");
    Console.WriteLine($"Card Sales: ₱{cardSales:F2}");

    Console.WriteLine("=================================");
    }
}