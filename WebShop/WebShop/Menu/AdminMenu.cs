using WebShop.Presentation.DisplayService;

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
public class AdminMenu
{
    bool _isRunning = true;
    private readonly AdminProdukt _adminProdukt;
    private readonly AdminKategori _adminKategori;

    public AdminMenu(AdminProdukt adminProdukt, AdminKategori adminKategori)
    {
        _adminProdukt = adminProdukt;
        _adminKategori = adminKategori;
    }


    public void ShowAdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin val!");
        Console.WriteLine("1 - Hantera produkter");
        Console.WriteLine("2 - *****");
        Console.WriteLine("6 - Tillbaka till startmenyn");
    }
    public async Task HandleInputAsync()
    {
       
            var input = Console.ReadLine();
            switch (input)
            {
            case "1":
                await _adminProdukt.ProduktMenuRunAsync();
                break;
            case "2":
                await _adminKategori.KategoriMenuRunAsync();
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
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