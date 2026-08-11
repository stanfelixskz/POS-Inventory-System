namespace POSInventorySystem.Models;

public abstract class Payment
{
    public decimal Amount { get; protected set; }

    protected Payment(decimal amount)
    {
        Amount = amount;
    }

    public abstract bool ProcessPayment();
}