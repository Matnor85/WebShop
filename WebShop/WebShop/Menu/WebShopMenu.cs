using WebShop.Presentation.DisplayService;
using WebShop.Presentation.Menu.Shop_Submenu;

namespace WebShop.Presentation.Menu;

public class WebShopMenu(ShoppingCartMenu shoppingCartMenu, SearchProductMenu searchProductMenu, BrowseCategoriesMenu browseCategoriesMenu, ManageOrderHistoryMenu manageOrderHistoryMenu, ShopKampanj kampanj)
{
    bool _isRunning = true;

    public async Task ShowWebShopMenu()
    {
        Console.Clear();
        Meny.WelcomeText();
        Meny.LineBreaks(3);
        Console.WriteLine("=== Webb-Shop val ===");
        Console.WriteLine("1 - Sök efter produkt");
        Console.WriteLine("2 - Bläddra bland kategorier");
        Console.WriteLine("****3 - Hantera användarprofiler och orderhistorik****");
        Console.WriteLine("4 - Hantera kundvagn");
        Console.WriteLine("5 - Tillbaka till startmenyn");
        Meny.LineBreaks(3);
        await ShowSales();
    }

    public void HandleInput()
    {

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                searchProductMenu.SearchProductRun();
                break;
            case "2":
                browseCategoriesMenu.BrowseCategoriesRun();
                break;
            case "3":
                //manageOrderHistoryMenu.ManageOrderHistoryRun();
                break;
            case "4":
                shoppingCartMenu.ShoppingCartRun();
                break;
            case "5":
                _isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }
    private async Task ShowSales()
    {
        await kampanj.ShowKampanjAsync();
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