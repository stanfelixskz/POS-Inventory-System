namespace POSInventorySystem.Models;


public class Transaction
{
private readonly List<CartItem> items = new List<CartItem>();

private static int transactionCounter = 1;

public string TransactionId { get; private set; }
public DateTime Date { get; private set; }

public IReadOnlyList<CartItem> Items => items;
public Payment? Payment { get; private set; }

public decimal Total
{
    get
    {
        return items.Sum(item => item.Subtotal);
    }
}

public Transaction()
{
    TransactionId = $"T{transactionCounter:D3}";
    transactionCounter++;
    Date = DateTime.Now;
}

public void AddItem(
    Product product,
    int quantity,
    BeverageCustomization? customization = null)
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

    items.Add(
        new CartItem(
            product,
            quantity,
            customization
        )
    );
}

public bool ProcessPayment(Payment payment)
{
    if (!payment.ProcessPayment())
    {
        return false;
    }

    Payment = payment;
    return true;
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
    Console.WriteLine();
    Console.WriteLine("========================================");
    Console.WriteLine("              STARBUCKS");
    Console.WriteLine("           SALES RECEIPT");
    Console.WriteLine("========================================");

    Console.WriteLine($"Transaction: {TransactionId}");
    Console.WriteLine(
        $"Date: {Date:MMM dd, yyyy hh:mm tt}"
    );

    Console.WriteLine("----------------------------------------");

    foreach (CartItem item in items)
    {
        Console.WriteLine(item.Product.Name);

        if (item.Customization != null)
        {
            Console.WriteLine(
                $"  {item.Customization.Size} / " +
                $"{item.Customization.Serving}"
            );

            Console.WriteLine(
                $"  {item.Customization.Milk}"
            );

            if (item.Customization.ExtraShots > 0)
            {
                Console.WriteLine(
                    $"  +{item.Customization.ExtraShots} Shot(s)"
                );
            }

            if (item.Customization.Syrup != "None")
            {
                Console.WriteLine(
                    $"  {item.Customization.Syrup} Syrup"
                );
            }
        }

        Console.WriteLine(
            $"  ₱{item.UnitPrice:F2} x{item.Quantity}"
        );

        Console.WriteLine(
            $"  Subtotal: ₱{item.Subtotal:F2}"
        );

        Console.WriteLine();
    }

    Console.WriteLine("----------------------------------------");

    Console.WriteLine(
        $"TOTAL:                 ₱{Total:F2}"
    );

    if (Payment != null)
    {
        Console.WriteLine("----------------------------------------");

        if (Payment is CashPayment cashPayment)
        {
            Console.WriteLine("Payment: Cash");

            Console.WriteLine(
                $"Amount Due:            ₱{cashPayment.Amount:F2}"
            );

            Console.WriteLine(
                $"Cash Received:         ₱{cashPayment.CashReceived:F2}"
            );

            Console.WriteLine(
                $"Change:                ₱{cashPayment.Change:F2}"
            );
        }
        else if (Payment is GCashPayment)
        {
            Console.WriteLine("Payment: GCash");

            Console.WriteLine(
                $"Amount Paid:           ₱{Payment.Amount:F2}"
            );
        }
        else if (Payment is CardPayment)
        {
            Console.WriteLine("Payment: Card");

            Console.WriteLine(
                $"Amount Paid:           ₱{Payment.Amount:F2}"
            );
        }
    }

    Console.WriteLine("----------------------------------------");
    Console.WriteLine("          Thank you for your");
    Console.WriteLine("             purchase!");
    Console.WriteLine("========================================");
    }
}