namespace POSInventorySystem.Models;

public class CartItem
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public BeverageCustomization? Customization { get; private set; }

    public decimal UnitPrice
    {
        get
        {
            decimal price = Product.Price;

            if (Customization != null)
            {
                price += Customization.GetAdditionalPrice();
            }

            return price;
        }
    }

    public decimal Subtotal
    {
        get
        {
            return UnitPrice * Quantity;
        }
    }

    public CartItem(
        Product product,
        int quantity,
        BeverageCustomization? customization = null)
    {
        Product = product;
        Quantity = quantity;
        Customization = customization;
    }

    public void DisplayCartItem()
    {
        Console.WriteLine(
            $"{Product.Name} x{Quantity} = ₱{Subtotal:F2}"
        );

        if (Customization != null)
        {
            Customization.DisplayCustomization();

            Console.WriteLine(
                $"Customization: +₱{Customization.GetAdditionalPrice():F2}"
            );

            Console.WriteLine(
                $"Unit Price: ₱{UnitPrice:F2}"
            );
        }
    }
}