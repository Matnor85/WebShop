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
   // Meny _meny = new Meny();
    bool _isRunning = true;
    public void ShowAdminMenu()
    {
        Console.Clear();
        Console.WriteLine("Admin val!");
        Console.WriteLine("1 - *****");
        Console.WriteLine("2 - *****");
        Console.WriteLine("3 - Tillbaka till startmenyn");
    }
    public void HandleInput()
    {
       
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    
                    break;
                case "2":
                    
                    break;
                case "3":
                    _isRunning = false;
                                    
                    break;
                case "4":
                    
                    break;
                case "5":
                    
                    break;
                case "6":
                    
                    break;
                case "7":
                    
                    break;
                case "8":
                    //Console.WriteLine("Tack för att du besökte vår webshop. Ha en bra dag!");
                   // _meny.MenuRun();
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        
    }
    public void AdminRun()
    {
        while (_isRunning)
        {
            ShowAdminMenu();
            HandleInput();
        }
    }
}