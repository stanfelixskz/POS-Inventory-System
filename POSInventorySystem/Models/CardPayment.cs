namespace POSInventorySystem.Models;

public class CardPayment : Payment
{
    public CardPayment(decimal amount)
        : base(amount)
    {
    }

    public override bool ProcessPayment()
    {
        Console.WriteLine("Card payment successful.");
        Console.WriteLine($"Amount: ₱{Amount:F2}");

        return true;
    }
}