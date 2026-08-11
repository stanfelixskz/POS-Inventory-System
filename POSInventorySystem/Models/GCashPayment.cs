namespace POSInventorySystem.Models;

public class GCashPayment : Payment
{
    public GCashPayment(decimal amount)
        : base(amount)
    {
    }

    public override bool ProcessPayment()
    {
        Console.WriteLine("GCash payment successful.");
        Console.WriteLine($"Amount: ₱{Amount:F2}");

        return true;
    }
}