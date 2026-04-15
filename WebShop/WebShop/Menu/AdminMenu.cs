using Webshop.Infrastructure.EF.Seeds;
using WebShop.Presentation.DisplayService;
using WebShop.Presentation.Menu.Submenu;

namespace WebShop.Presentation.Menu;

public class AdminMenu(KategoriMenu kategoriMenu, ProduktMenu produktMenu, KundMenu kundMenu, OrderMenu orderMenu, KampanjerMenu kampanjerMenu, SeederGenerator seeder)
{
    bool _isRunning = true;
    public void ShowAdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin val!");
        Console.WriteLine("1 - Hantera produkter");
        Console.WriteLine("2 - Hantera kategorier");
        Console.WriteLine("3 - Hantera kunder");
        Console.WriteLine("4 - Hantera ordrar");
        Console.WriteLine("5 - Hantera kampanjer");
        Console.WriteLine("6 - Lägg till Seed-data");
        Meny.LineBreaks(2);
        Console.WriteLine("0 - Tillbaka till startmenyn");
    }
    public async Task HandleInputAsync()
    {

        var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "1":
                await produktMenu.ProduktMenuRunAsync();
                break;
            case "2":
                await kategoriMenu.KategoriMenuRunAsync();
                break;
            case "3":
                await kundMenu.KundMenuRunAsync();
                break;
            case "4":
                await orderMenu.OrderMenuRunAsync();
                break;
            case "5":
                await kampanjerMenu.KampanjerMenuRunAsync();
                break;
            case "6":
                try
                {
                    await seeder.SeedAsync();
                    Console.WriteLine("Seeding lyckades.");
                    Meny.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Seeding misslyckades: {ex.Message}\n{ex.StackTrace}");
                    Meny.Wait();
                }
                break;
            case "0":
                _isRunning = false;
                return;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }

    }
    public async Task AdminRunAsync()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShowAdminMenu();
            await HandleInputAsync();
        }
    }
}