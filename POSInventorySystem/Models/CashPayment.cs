namespace POSInventorySystem.Models;

public class CashPayment : Payment
{
public decimal CashReceived { get; private set; }

public decimal Change
{
    get
    {
        return CashReceived - Amount;
    }
}

public CashPayment(decimal amount, decimal cashReceived)
    : base(amount)
{
    CashReceived = cashReceived;
}

public override bool ProcessPayment()
{
    if (CashReceived < Amount)
    {
        Console.WriteLine("Insufficient cash.");
        return false;
    }

    Console.WriteLine("Cash payment successful.");
    Console.WriteLine($"Amount: ₱{Amount:F2}");
    Console.WriteLine($"Cash received: ₱{CashReceived:F2}");
    Console.WriteLine($"Change: ₱{Change:F2}");

    return true;
}

}