using POSInventorySystem.Data;
using POSInventorySystem.Models;

namespace POSInventorySystem;

public class CashierMenu
{
    public void Show()
    {
        while (true)
        {
            Transaction transaction = new Transaction();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("      STARBUCKS POS SYSTEM");
                Console.WriteLine("=================================");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Exit");
                Console.WriteLine("=================================");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                Console.WriteLine();

                if (choice == "1")
                {
                    Console.WriteLine("========================================");
                    Console.WriteLine("              PRODUCTS");
                    Console.WriteLine("========================================");
                    Console.WriteLine(
                        $"{"ID",-6} {"PRODUCT",-25} {"PRICE",10}"
                    );
                    Console.WriteLine("----------------------------------------");

                    foreach (Product product in DataStore.Products)
                    {
                        product.DisplayPOSItem();
                    }

                    Console.WriteLine("========================================");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine("       ADD PRODUCT TO CART");
                    Console.WriteLine("=================================");

                    Console.Write("Enter Product ID: ");

                    string? productId = Console.ReadLine();

                    Product? product = DataStore.Products.FirstOrDefault(
                        p => p.ProductId.Equals(
                            productId,
                            StringComparison.OrdinalIgnoreCase)
                    );

                    if (product == null)
                    {
                        Console.WriteLine("Product not found.");
                        continue;
                    }

                    BeverageCustomization? customization = null;

                    if (product is BeverageProduct)
                    {
                        customization = CreateBeverageCustomization();
                    }

                    Console.Write("Enter quantity: ");

                    if (!int.TryParse(
                        Console.ReadLine(),
                        out int quantity))
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }

                    if (quantity <= 0)
                    {
                        Console.WriteLine(
                            "Quantity must be greater than zero."
                        );
                        continue;
                    }

                    transaction.AddItem(
                        product,
                        quantity,
                        customization
                    );

                    Console.WriteLine();
                    Console.WriteLine("Product added to cart.");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine("          CURRENT CART");
                    Console.WriteLine("=================================");

                    if (transaction.Items.Count == 0)
                    {
                        Console.WriteLine("Cart is empty.");
                    }
                    else
                    {
                        foreach (CartItem item in transaction.Items)
                        {
                            item.DisplayCartItem();
                            Console.WriteLine();
                        }

                        Console.WriteLine(
                            $"TOTAL: ₱{transaction.Total:F2}"
                        );
                    }
                }
                else if (choice == "4")
                {
                    if (transaction.Items.Count == 0)
                    {
                        Console.WriteLine("Cart is empty.");
                        continue;
                    }

                    Console.WriteLine("CHECKOUT");
                    Console.WriteLine("=================================");
                    Console.WriteLine(
                        $"TRANSACTION ID: {transaction.TransactionId}"
                    );
                    Console.WriteLine(
                        $"TOTAL: ₱{transaction.Total:F2}"
                    );
                    Console.WriteLine();

                    Console.WriteLine("Select Payment Method:");
                    Console.WriteLine("1. Cash");
                    Console.WriteLine("2. GCash");
                    Console.WriteLine("3. Card");
                    Console.Write("Choose payment method: ");

                    string? paymentChoice = Console.ReadLine();

                    Payment? payment = null;

                    if (paymentChoice == "1")
                    {
                        Console.Write("Enter cash received: ");

                        if (!decimal.TryParse(
                            Console.ReadLine(),
                            out decimal cashReceived))
                        {
                            Console.WriteLine("Invalid amount.");
                            continue;
                        }

                        payment = new CashPayment(
                            transaction.Total,
                            cashReceived
                        );
                    }
                    else if (paymentChoice == "2")
                    {
                        payment = new GCashPayment(
                            transaction.Total
                        );
                    }
                    else if (paymentChoice == "3")
                    {
                        payment = new CardPayment(
                            transaction.Total
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            "Invalid payment method."
                        );
                        continue;
                    }

                    if (!transaction.ProcessPayment(payment))
                    {
                        Console.WriteLine("Payment failed.");
                        continue;
                    }

                    if (!transaction.CompleteTransaction())
                    {
                        Console.WriteLine(
                            "Transaction failed."
                        );
                        continue;
                    }

                    DataStore.Transactions.Add(transaction);

                    Console.WriteLine();
                    Console.WriteLine(
                        "Transaction completed successfully!"
                    );

                    Console.WriteLine();

                    transaction.DisplayReceipt();

                    Console.WriteLine();
                    Console.WriteLine("Updated Inventory:");

                    foreach (CartItem item in transaction.Items)
                    {
                        Console.WriteLine(
                            $"{item.Product.Name}: " +
                            $"{item.Product.Stock} remaining"
                        );
                    }

                    Console.WriteLine();
                    Console.WriteLine(
                        "Starting new transaction..."
                    );

                    break;
                }
                else if (choice == "5")
                {
                    Console.WriteLine(
                        "Thank you for using the POS."
                    );

                    return;
                }
                else
                {
                    Console.WriteLine(
                        "Invalid option."
                    );
                }
            }
        }
    }

private BeverageCustomization CreateBeverageCustomization()
{
    string size;

    // SIZE
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Select Size:");
        Console.WriteLine("1. Tall");
        Console.WriteLine("2. Grande");
        Console.WriteLine("3. Venti");
        Console.Write("Choose size: ");

        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            size = "Tall";
            break;
        }

        if (choice == "2")
        {
            size = "Grande";
            break;
        }

        if (choice == "3")
        {
            size = "Venti";
            break;
        }

        Console.WriteLine("Invalid size.");
    }

    string serving;

    // SERVING
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Select Serving:");
        Console.WriteLine("1. Hot");
        Console.WriteLine("2. Iced");
        Console.WriteLine("3. Blended");
        Console.Write("Choose serving: ");

        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            serving = "Hot";
            break;
        }

        if (choice == "2")
        {
            serving = "Iced";
            break;
        }

        if (choice == "3")
        {
            serving = "Blended";
            break;
        }

        Console.WriteLine("Invalid serving selection.");
    }

    string milk;

    // MILK
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Select Milk:");
        Console.WriteLine("1. Whole Milk");
        Console.WriteLine("2. Soy Milk (+₱20)");
        Console.WriteLine("3. Oat Milk (+₱20)");
        Console.WriteLine("4. Almond Milk (+₱20)");
        Console.WriteLine("5. None");
        Console.Write("Choose milk: ");

        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            milk = "Whole Milk";
            break;
        }

        if (choice == "2")
        {
            milk = "Soy Milk";
            break;
        }

        if (choice == "3")
        {
            milk = "Oat Milk";
            break;
        }

        if (choice == "4")
        {
            milk = "Almond Milk";
            break;
        }

        if (choice == "5")
        {
            milk = "None";
            break;
        }

        Console.WriteLine("Invalid milk selection.");
    }

    int extraShots;

    // EXTRA SHOTS
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Extra Espresso Shots:");
        Console.WriteLine("0. None");
        Console.WriteLine("1. +1 Shot (+₱30)");
        Console.WriteLine("2. +2 Shots (+₱60)");
        Console.WriteLine("3. +3 Shots (+₱90)");
        Console.Write("Choose extra shots: ");

        string? choice = Console.ReadLine();

        if (choice == "0")
        {
            extraShots = 0;
            break;
        }

        if (choice == "1")
        {
            extraShots = 1;
            break;
        }

        if (choice == "2")
        {
            extraShots = 2;
            break;
        }

        if (choice == "3")
        {
            extraShots = 3;
            break;
        }

        Console.WriteLine("Invalid extra shot selection.");
    }

    string syrup;

    // SYRUP
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Select Syrup:");
        Console.WriteLine("1. None");
        Console.WriteLine("2. Vanilla (+₱15)");
        Console.WriteLine("3. Caramel (+₱15)");
        Console.WriteLine("4. Hazelnut (+₱15)");
        Console.Write("Choose syrup: ");

        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            syrup = "None";
            break;
        }

        if (choice == "2")
        {
            syrup = "Vanilla";
            break;
        }

        if (choice == "3")
        {
            syrup = "Caramel";
            break;
        }

        if (choice == "4")
        {
            syrup = "Hazelnut";
            break;
        }

        Console.WriteLine("Invalid syrup selection.");
    }

    return new BeverageCustomization(
        size,
        serving,
        milk,
        extraShots,
        syrup
        );
    }
}