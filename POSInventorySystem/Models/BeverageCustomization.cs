namespace POSInventorySystem.Models;

public class BeverageCustomization
{
    public string Size { get; private set; }
    public string Serving { get; private set; }
    public string Milk { get; private set; }
    public int ExtraShots { get; private set; }
    public string Syrup { get; private set; }

    public BeverageCustomization(
        string size,
        string serving,
        string milk,
        int extraShots,
        string syrup)
    {
        Size = size;
        Serving = serving;
        Milk = milk;
        ExtraShots = extraShots;
        Syrup = syrup;
    }

    public decimal GetAdditionalPrice()
    {
        decimal additionalPrice = 0m;

        // Size pricing
        if (Size == "Grande")
        {
            additionalPrice += 10m;
        }
        else if (Size == "Venti")
        {
            additionalPrice += 20m;
        }

        // Milk pricing
        if (Milk == "Soy Milk" ||
            Milk == "Oat Milk" ||
            Milk == "Almond Milk")
        {
            additionalPrice += 20m;
        }

        // Extra espresso shots
        additionalPrice += ExtraShots * 30m;

        // Syrup pricing
        if (Syrup != "None")
        {
            additionalPrice += 15m;
        }

        return additionalPrice;
    }

    public void DisplayCustomization()
    {
        Console.WriteLine($"Size: {Size}");
        Console.WriteLine($"Serving: {Serving}");
        Console.WriteLine($"Milk: {Milk}");
        Console.WriteLine($"Extra Shots: {ExtraShots}");
        Console.WriteLine($"Syrup: {Syrup}");
    }
}