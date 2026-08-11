namespace POSInventorySystem.Models;

public class Transaction
{
    private readonly List<CartItem> items = new List<CartItem>();

    public string TransactionId { get; private set; }
    public DateTime Date { get; private set; }

    public IReadOnlyList<CartItem> Items => items;

    public decimal Total
    {
        get
        {
            return items.Sum(item => item.Subtotal);
        }
    }

    public Transaction(string transactionId)
    {
        TransactionId = transactionId;
        Date = DateTime.Now;
    }

    public void AddItem(Product product, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Invalid quantity.");
            return;
        }

        CartItem? existingItem = items.FirstOrDefault(
            item => item.Product.ProductId == product.ProductId
        );

        if (existingItem != null)
        {
            Console.WriteLine("Product is already in the cart.");
            return;
        }

        items.Add(new CartItem(product, quantity));
    }
    public bool CompleteTransaction()
{
    foreach (CartItem item in items)
    {
        if (item.Product.Stock < item.Quantity)
        {
            Console.WriteLine(
                $"Not enough stock for {item.Product.Name}."
            );

            return false;
        }
    }

    foreach (CartItem item in items)
    {
        item.Product.RemoveStock(item.Quantity);
    }

    return true;
}

    public void DisplayReceipt()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("             RECEIPT");
        Console.WriteLine("=================================");
        Console.WriteLine($"Transaction ID: {TransactionId}");
        Console.WriteLine($"Date: {Date}");
        Console.WriteLine();

        foreach (CartItem item in items)
        {
            item.DisplayCartItem();
        }

        Console.WriteLine();
        Console.WriteLine($"TOTAL: ₱{Total:F2}");
        Console.WriteLine("=================================");
    }
}