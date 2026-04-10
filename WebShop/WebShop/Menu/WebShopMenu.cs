using WebShop.Presentation.DisplayService;
using WebShop.Presentation.Menu.Shop_Submenu;

namespace WebShop.Presentation.Menu;
/// <summary>
/// 1. Visa webb-shopmeny
/// 2. Hantera webb-shopval
/// 3. Implementera funktionalitet för att visa produkter, kategorier och kampanjer
/// 4. Implementera funktionalitet för att hantera kundvagn och beställningar
/// 5. Implementera funktionalitet för att hantera användarprofiler och orderhistorik
/// </summary>
public class WebShopMenu(ShoppingCartMenu shoppingCartMenu, SearchProductMenu searchProductMenu, BrowseCategoriesMenu browseCategoriesMenu)
{
    bool _isRunning = true;

    public void ShowWebShopMenu()
    {
        Console.Clear();
        WelcomeText();
        Meny.LineBreaks(3);
        Console.WriteLine("=== Webb-Shop val ===");
        Console.WriteLine("1 - Sök efter produkt");
        Console.WriteLine("2 - Bläddra bland kategorier");
        Console.WriteLine("3 - Hantera användarprofiler och orderhistorik");
        Console.WriteLine("4 - Hantera kundvagn");
        Console.WriteLine("5 - Tillbaka till startmenyn");
        Meny.LineBreaks(3);
        ShowSales();
    }

    public void HandleInput()
    {

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                SearchProduct();
                break;
            case "2":
                BrowseCategories();
                break;
            case "3":
                ManageOrderHistory();
                break;
            case "4":
                ShoppingCart();
                break;
            case "5":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }

    }
    
    private void ShoppingCart()
    {
        Console.WriteLine("=== Kundvagn ===");
        
        foreach (var item in ShoppingCartService.CartItems)
        {
            Console.WriteLine($"{item.Product.Name} - Antal: {item.Quantity} - Pris: {item.Product.Price * item.Quantity}");
        }
        ShoppingCartMenu();
    }

    private void ShoppingCartMenu()
    {
        Console.WriteLine("=== Kundvagn val ===");
        Console.WriteLine("1 - Ändra antal");
        Console.WriteLine("2 - Ta bort produkt");
        Console.WriteLine("3 - Gå vidare till kassan");
        Console.WriteLine("4 - Tillbaka till webb-shopmenyn");
    }

    private void ManageOrderHistory()
    {
        Console.WriteLine("=== Användarprofiler och orderhistorik ===");

    }

    private async Task BrowseCategories()
    {
        Console.WriteLine("=== Kategorier ===");
        try
        {
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private void SearchProduct()
    {
        Console.WriteLine("=== Sök produkt ===");

    }

    private void WelcomeText()
    {
        Console.WriteLine("Välkommen till vår webbshop!");
        Console.WriteLine("Här kan du hitta de senaste produkterna och erbjudandena.");
    }

    private void ShowSales()
    {
        Console.WriteLine("Erbjudanden och kampanjer:");
        Console.WriteLine("*****************");
        Console.WriteLine("*****************");
        Console.WriteLine("*****************");
    }
    public void WebbRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            ShowWebShopMenu();
            HandleInput();

        }
    }
}