using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.ShopService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class BrowseCategoriesMenu(IKategoriService kategoriService, ShoppingCartMenu shoppingCartMenu, ShopBrowseCategories shopBrowseCategories)
{
    private bool _isRunning = true;
    private List<Kategori> _categories = new();     

    public async Task BrowseCategoriesRun()
    {
        _categories = await kategoriService.GetAllAsync();
        _isRunning = true;

        while (_isRunning)
        {
            ShowMenu();
            await HandleInput();
        }
    }

    private void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Bläddra bland Kategorier ===");

        for (int i = 0; i < _categories.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_categories[i].Namn}");
        }

        Meny.CreateLine('-', 30);
        Meny.LineBreaks(1);
        Console.WriteLine("0 - Tillbaka till huvudmenyn   [K] - Gå till kundvagn");
        Console.Write("Välj kategori: ");
    }

    public async Task HandleInput()
    {
       var input = Console.ReadLine()?.Trim().ToLower();
       if (input == "0")
       {
           _isRunning = false;
           return;
       }
        else if (input == "k")
        {
            await shoppingCartMenu.ShoppingCartRun();
            _isRunning = false;
            return;
        }
        if (int.TryParse(input, out int choice) && choice > 0 && choice <= _categories.Count)
        {
            var selectedCategory = _categories[choice - 1];
            await shopBrowseCategories.ShowProducts(selectedCategory);
        }
        else
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            Console.ReadLine();
        }
    }
}