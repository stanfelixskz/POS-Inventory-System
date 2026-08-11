namespace POSInventorySystem.Models;

public class BeverageProduct : Product
{
    public BeverageProduct(
        string productId,
        string name,
        decimal price,
        int stock,
        string category)
        : base(productId, name, price, stock, category)
    {
    }

    public override void DisplayDetails()
    {
        Console.WriteLine("----- BEVERAGE PRODUCT -----");
        base.DisplayDetails();
    }
}