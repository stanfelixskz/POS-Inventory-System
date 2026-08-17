using POSInventorySystem.Models;

namespace POSInventorySystem.Data;

public static class DataStore
{
public static List<Product> Products { get; } = new List<Product>
{
    new BeverageProduct(
        "P001",
        "Caffe Latte",
        150.00m,
        10,
        "Beverage"
    ),

    new BeverageProduct(
        "P002",
        "Caramel Macchiato",
        170.00m,
        8,
        "Beverage"
    ),

    new BeverageProduct(
        "P003",
        "Java Chip Frappuccino",
        180.00m,
        7,
        "Beverage"
    ),

    new BeverageProduct(
        "P004",
        "Caffe Americano",
        135.00m,
        10,
        "Beverage"
    ),

    new BeverageProduct(
        "P005",
        "Mocha Frappuccino",
        175.00m,
        8,
        "Beverage"
    ),

    new BeverageProduct(
        "P006",
        "Iced Caffe Latte",
        155.00m,
        10,
        "Beverage"
    ),

    new FoodProduct(
        "P007",
        "Chocolate Chip Cookie",
        95.00m,
        12,
        "Food"
    ),

    new FoodProduct(
        "P008",
        "Chocolate Croissant",
        120.00m,
        6,
        "Food"
    ),

    new FoodProduct(
        "P009",
        "Chicken Pesto Sandwich",
        165.00m,
        5,
        "Food"
    ),

    new FoodProduct(
        "P010",
        "Sausage Roll",
        110.00m,
        6,
        "Food"
        )
    };

    public static List<Transaction> Transactions { get; } =
        new List<Transaction>();

public static List<User> Users { get; } = new List<User>
{
    new User(
        "admin",
        "admin123",
        "Administrator",
        "Admin"
    ),

    new User(
        "cashier",
        "cashier123",
        "Cashier",
        "Cashier"
        )
    };
}