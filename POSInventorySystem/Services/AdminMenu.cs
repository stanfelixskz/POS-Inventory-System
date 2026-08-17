using POSInventorySystem.Services;

namespace POSInventorySystem;

public class AdminMenu
{
    private readonly AdminService adminService;

    public AdminMenu()
    {
        adminService = new AdminService();
    }

    public void Show()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("       ADMINISTRATOR MENU");
            Console.WriteLine("=================================");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Restock Product");
            Console.WriteLine("5. Exit");
            Console.WriteLine("=================================");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

switch (choice)
{
    case "1":
        adminService.ViewInventory();
        break;

    case "2":
        adminService.AddProduct();
        break;

    case "3":
        adminService.UpdateProduct();
        break;

    case "4":
        adminService.RestockProduct();
        break;

    case "5":
        adminService.ViewLowStock();
        break;

    case "6":
        running = false;
        break;

    default:
        Console.WriteLine("Invalid option.");
        break;
            }
        }
    }
}