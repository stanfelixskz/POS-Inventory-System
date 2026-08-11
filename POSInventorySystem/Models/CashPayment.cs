namespace POSInventorySystem.Models;

public class CashPayment : Payment
{
    private decimal cashReceived;

    public CashPayment(decimal amount, decimal cashReceived)
        : base(amount)
    {
        this.cashReceived = cashReceived;
    }

    public override bool ProcessPayment()
    {
        if (cashReceived < Amount)
        {
            Console.WriteLine("Insufficient cash.");
            return false;
        }

        decimal change = cashReceived - Amount;

        Console.WriteLine("Cash payment successful.");
        Console.WriteLine($"Amount: ₱{Amount:F2}");
        Console.WriteLine($"Cash received: ₱{cashReceived:F2}");
        Console.WriteLine($"Change: ₱{change:F2}");

        return true;
    }
}