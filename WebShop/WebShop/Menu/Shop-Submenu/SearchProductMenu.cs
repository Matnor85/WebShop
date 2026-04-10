using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class SearchProductMenu(ShopSearchProduct searchProduct)
{
    bool _isRunning = true;
    public void SearchProduct()
    {
        Console.WriteLine("=== Sök produkt ===");
        Console.WriteLine("Namn: ");
    }
    public void HandleInput()
    {
        var input = Console.ReadLine();
        if (input == null) {
            return;
        }
    }
    public void SearchProductRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            SearchProduct();
            HandleInput();
        }
    }
}
