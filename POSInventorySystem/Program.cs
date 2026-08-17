using POSInventorySystem.Data;
using POSInventorySystem.Models;

Transaction transaction = new Transaction();

while (true)
{
Console.WriteLine("=================================");
Console.WriteLine("      STARBUCKS POS SYSTEM");
Console.WriteLine("=================================");
Console.WriteLine("1. View Products");
Console.WriteLine("2. Add Product to Cart");
Console.WriteLine("3. View Cart");
Console.WriteLine("4. Checkout");
Console.WriteLine("5. Exit");
Console.WriteLine("=================================");
Console.Write("Choose an option: ");

string? choice = Console.ReadLine();

Console.WriteLine();

if (choice == "1")
{
    Console.WriteLine("PRODUCTS");
    Console.WriteLine("=================================");

    foreach (Product product in DataStore.Products)
    {
        product.DisplayDetails();
        Console.WriteLine();
    }
}
else if (choice == "2")
{
    Console.WriteLine("Add Product to Cart");
    Console.Write("Enter Product ID: ");

    string? productId = Console.ReadLine();

    Product? product = DataStore.Products.FirstOrDefault(
        p => p.ProductId == productId
    );

    if (product == null)
    {
        Console.WriteLine("Product not found.");
        continue;
    }

    Console.Write("Enter quantity: ");

    if (!int.TryParse(Console.ReadLine(), out int quantity))
    {
        Console.WriteLine("Invalid quantity.");
        continue;
    }

    transaction.AddItem(product, quantity);

    Console.WriteLine("Product added to cart.");
}
else if (choice == "3")
{
    Console.WriteLine("CURRENT CART");
    Console.WriteLine("=================================");

    foreach (CartItem item in transaction.Items)
    {
        item.DisplayCartItem();
    }

    Console.WriteLine();
    Console.WriteLine($"TOTAL: ₱{transaction.Total:F2}");
}
else if (choice == "4")
{
if (transaction.Items.Count == 0)
{
Console.WriteLine("Cart is empty.");
continue;
}

Console.WriteLine("CHECKOUT");
Console.WriteLine("=================================");
Console.WriteLine($"TOTAL: ₱{transaction.Total:F2}");
Console.WriteLine();

Console.WriteLine("Select Payment Method:");
Console.WriteLine("1. Cash");
Console.WriteLine("2. GCash");
Console.WriteLine("3. Card");
Console.Write("Choose payment method: ");

string? paymentChoice = Console.ReadLine();

Payment? payment = null;

if (paymentChoice == "1")
{
    Console.Write("Enter cash received: ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal cashReceived))
    {
        Console.WriteLine("Invalid amount.");
        continue;
    }

    payment = new CashPayment(transaction.Total, cashReceived);
}
else if (paymentChoice == "2")
{
    payment = new GCashPayment(transaction.Total);
}
else if (paymentChoice == "3")
{
    payment = new CardPayment(transaction.Total);
}
else
{
    Console.WriteLine("Invalid payment method.");
    continue;
}

if (!transaction.ProcessPayment(payment))
{
    Console.WriteLine("Payment failed.");
    continue;
}

if (!transaction.CompleteTransaction())
{
    Console.WriteLine("Transaction failed.");
    continue;
}

DataStore.Transactions.Add(transaction);

Console.WriteLine("Transaction completed successfully.");
Console.WriteLine();

transaction.DisplayReceipt();

Console.WriteLine();
Console.WriteLine("Updated Inventory:");

foreach (CartItem item in transaction.Items)
{
    Console.WriteLine(
        $"{item.Product.Name}: {item.Product.Stock} remaining"
    );
}

break;

}
else if (choice == "5")
{
    Console.WriteLine("Thank you for using the POS.");
    break;
}
else
{
    Console.WriteLine("Invalid option.");
}

}