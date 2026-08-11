using POSInventorySystem.Models;

namespace POSInventorySystem.Data;

public static class DataStore
{
    public static List<Product> Products { get; } = new List<Product>
    {
        new FoodProduct(
            "P001",
            "Chicken Sandwich",
            120.00m,
            10,
            "Food"
        ),

        new FoodProduct(
            "P002",
            "French Fries",
            80.00m,
            15,
            "Food"
        ),

        new BeverageProduct(
            "P003",
            "Iced Coffee",
            95.00m,
            8,
            "Beverage"
        ),

        new BeverageProduct(
            "P004",
            "Milk Tea",
            110.00m,
            5,
            "Beverage"
        )
    };
}