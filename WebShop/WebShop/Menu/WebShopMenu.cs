using WebShop.Presentation.DisplayService.ShopService;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu.Shop_Submenu;

namespace WebShop.Presentation.Menu;

public class WebShopMenu(ShoppingCartMenu shoppingCartMenu, ShopSearchProduct searchProduct, BrowseCategoriesMenu browseCategoriesMenu, ShopKampanj kampanj, ValutaDisplay valutaDisplay)
{
    bool _isRunning = true;

    public async Task ShowWebShopMenu()
    {
        Console.Clear();
        Meny.WelcomeText();
        Meny.LineBreaks(2);
        Console.WriteLine("=== Webb-Shop val ===");
        Console.WriteLine("1 - Sök efter produkt");
        Console.WriteLine("2 - Bläddra bland kategorier");
        Console.WriteLine("3 - Hantera kundvagn");
        Console.WriteLine("4 - Välj valuta");
        Meny.LineBreaks(2);
        Console.WriteLine("[Esc] - Tillbaka till startmenyn");
        Meny.LineBreaks(2);
        await ShowSales();
    }

    public async Task HandleInput()
    {
var input = Console.ReadLine()?.Trim().ToLower();
        switch (input)
        {
            case "1":
                await searchProduct.SearchProductRun();
                break;
            case "2":
                await browseCategoriesMenu.BrowseCategoriesRun();
                break;
            case "3":
               await shoppingCartMenu.ShoppingCartRun();
                break;
            case "4":
                await valutaDisplay.ChooseRateAsync();
                break;
            case "esc":
            case "escape":
            case "avbryt":
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