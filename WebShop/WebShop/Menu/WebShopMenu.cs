namespace WebShop.Presentation.Menu;
/// <summary>
/// 1. Visa webb-shopmeny
/// 2. Hantera webb-shopval
/// 3. Implementera funktionalitet för att visa produkter, kategorier och kampanjer
/// 4. Implementera funktionalitet för att hantera kundvagn och beställningar
/// 5. Implementera funktionalitet för att hantera användarprofiler och orderhistorik
/// </summary>
public class WebShopMenu
{
    //Meny _meny = new Meny();
    bool _isRunning = true;
    public void ShowWebShopMenu()
    {
        Console.Clear();
        Console.WriteLine("Webb-Shop val!");
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
                   // Console.WriteLine("Tack för att du besökte vår webshop. Ha en bra dag!");
                    //_meny.MenuRun();
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
        
    }
    public void WebbRun()
    {
        while (_isRunning)
        {
            ShowWebShopMenu();
            HandleInput();
        }
    }
}