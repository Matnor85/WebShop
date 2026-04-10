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
public class AdminMenu(KategoriMenu kategoriMenu, ProduktMenu produktMenu, KundMenu kundMenu, OrderMenu orderMenu)
{
    bool _isRunning = true;
    private readonly KategoriMenu _kategoriMenu;
    private readonly ProduktMenu _produktMenu;
    private readonly KundMenu _kundMenu;
    private readonly SeederGenerator _seeder;
    public AdminMenu(SeederGenerator seeder) { _seeder = seeder; }

    public AdminMenu(KategoriMenu kategoriMenu, ProduktMenu produktMenu, KundMenu kundMenu)
    {
        _kategoriMenu = kategoriMenu;
        _produktMenu = produktMenu;
        _kundMenu = kundMenu;
    }


    public void ShowAdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin val!");
        Console.WriteLine("1 - Hantera produkter");
        Console.WriteLine("2 - Hantera kategorier");
        Console.WriteLine("3 - Hantera kunder");
        Console.WriteLine("4 - Hantera ordrar");
        Console.WriteLine("5 - Lägg till Seed-data");
        Console.WriteLine("6 - Tillbaka till startmenyn");
    }
    public async Task HandleInputAsync()
    {
       
            var input = Console.ReadLine();
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
                try
                {
                    await _seeder.SeedAsync();
                    Console.WriteLine("Seeding lyckades.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Seeding misslyckades: {ex.Message}\n{ex.StackTrace}");
                }
                break;
            case "6":
                    _isRunning = false;
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        
    }
    public async Task AdminRunAsync()
    {
        while (_isRunning)
        {   
            ShowAdminMenu();
            await HandleInputAsync();
        }
    }
}