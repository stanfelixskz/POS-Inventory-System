using POSInventorySystem.Data;
using POSInventorySystem.Models;

Transaction transaction = new Transaction("T001");

Product? chicken = DataStore.Products.FirstOrDefault(
    product => product.ProductId == "P001"
);

Product? coffee = DataStore.Products.FirstOrDefault(
    product => product.ProductId == "P003"
);

if (chicken != null)
{
    transaction.AddItem(chicken, 2);
}

if (coffee != null)
{
    transaction.AddItem(coffee, 1);
}

if (transaction.CompleteTransaction())
{
    Console.WriteLine("Transaction completed successfully.");
    Console.WriteLine();

    transaction.DisplayReceipt();

    Console.WriteLine();
    Console.WriteLine("Updated Inventory:");
    Console.WriteLine(
        $"{chicken?.Name}: {chicken?.Stock} remaining"
    );

    Console.WriteLine(
        $"{coffee?.Name}: {coffee?.Stock} remaining"
    );
}