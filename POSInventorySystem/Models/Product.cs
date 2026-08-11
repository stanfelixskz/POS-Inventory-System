namespace POSInventorySystem.Models;

public class Product
{
    public string ProductId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Category { get; private set; }

    public Product(
        string productId,
        string name,
        decimal price,
        int stock,
        string category)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Stock = stock;
        Category = category;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Product ID : {ProductId}");
        Console.WriteLine($"Name       : {Name}");
        Console.WriteLine($"Price      : ₱{Price:F2}");
        Console.WriteLine($"Stock      : {Stock}");
        Console.WriteLine($"Category   : {Category}");
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Invalid stock quantity.");
            return;
        }

        Stock += quantity;
    }

    public bool IsLowStock()
    {
        return Stock <= 5;
    }
    public bool RemoveStock(int quantity)
{
    if (quantity <= 0 || quantity > Stock)
    {
        return false;
    }

    Stock -= quantity;
    return true;
}
}