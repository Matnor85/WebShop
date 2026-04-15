using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ShopService;
using WebShop.Presentation.DisplayService.ValutaApi;

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

        Meny.CreateLines('-', 30);
        Meny.LineBreaks(1);
        Console.WriteLine("Skriv 'avbryt' för att gå Tillbaka till huvudmenyn   [K] - Gå till kundvagn");
        Console.Write("Välj kategori: ");
    }

    public async Task HandleInput()
    {
       // ConsoleKeyInfo key = Console.ReadKey(true);
        var input = Console.ReadLine()?.Trim().ToLower();
        if (input == "avbryt" || input == "esc" || input == "escape")
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