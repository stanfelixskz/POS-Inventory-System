namespace POSInventorySystem.Models;

public class CartItem
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public decimal Subtotal
    {
        get
        {
            return Product.Price * Quantity;
        }
    }

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public void DisplayCartItem()
    {
        Console.WriteLine(
            $"{Product.Name} x{Quantity} = ₱{Subtotal:F2}"
        );
    }
}