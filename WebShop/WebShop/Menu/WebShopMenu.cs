using WebShop.Presentation.DisplayService.ShopService;
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
        //Console.WriteLine("****3 - Hantera användarprofiler och orderhistorik****");
        Console.WriteLine("3 - Hantera kundvagn");
        Console.WriteLine("4 - Tillbaka till startmenyn");
        Meny.LineBreaks(3);
        await ShowSales();
    }

    public async Task HandleInput()
    {

        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                await searchProductMenu.SearchProductRun();
                break;
            case "2":
                await browseCategoriesMenu.BrowseCategoriesRun();
                break;
            case "3":
               await shoppingCartMenu.ShoppingCartRunAsync();
                break;
            case "4":
                //manageOrderHistoryMenu.ManageOrderHistoryRun();
                //  break;
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
    public async Task WebbRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            await ShowWebShopMenu();
            await HandleInput();
        }
    }
}