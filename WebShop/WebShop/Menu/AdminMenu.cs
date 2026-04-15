using Webshop.Infrastructure.EF.Seeds;
using WebShop.Presentation.DisplayService;
using WebShop.Presentation.Menu.Submenu;

namespace WebShop.Presentation.Menu;
/// <summary>
/// 1. Visa adminmeny
/// 2. Hantera adminval
/// 3. Implementera funktionalitet för att hantera produkter, kategorier, användare och beställningar
/// 4. Implementera funktionalitet för att visa statistik och rapporter
/// 5. Implementera funktionalitet för att hantera kampanjer och rabatter
/// 6. Implementera funktionalitet för att hantera fraktalternativ och leveransstatus
/// 
/// </summary>
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
        Console.WriteLine("[Esc] - Tillbaka till startmenyn");
    }
    public async Task HandleInputAsync()
    {

        ConsoleKeyInfo key = Console.ReadKey(true);
        switch (key.Key)
            {
            case ConsoleKey.D1:
                await produktMenu.ProduktMenuRunAsync();
                break;
            case ConsoleKey.D2:
                await kategoriMenu.KategoriMenuRunAsync();
                break;
            case ConsoleKey.D3:
                await kundMenu.KundMenuRunAsync();
                break;
            case ConsoleKey.D4:
                await orderMenu.OrderMenuRunAsync();
                break;
            case ConsoleKey.D5:
                await kampanjerMenu.KampanjerMenuRunAsync();
                break;
            case ConsoleKey.D6:
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
            case ConsoleKey.Escape:
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