using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class BrowseCategoriesMenu(WebshopDbContext context)
{
    private bool _isRunning = true;
    private List<Kategori> _categories = new(); // Cache för att slippa hämta från DB vid varje input-loop

    public async Task BrowseCategoriesRun()
    {
        // Hämta kategorier en gång när menyn startar
        _categories = await context.Kategorier.ToListAsync();
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
        Console.WriteLine("0 - Tillbaka till huvudmenyn");
        Console.Write("\nVal: ");
    }

    public async Task HandleInput()
    {
        var input = Console.ReadLine();

        // 1. Hantera "Gå tillbaka" separat (här använder vi 0 för att inte krocka med kategori 1-19)
        if (input == "0")
        {
            _isRunning = false;
            return;
        }

        // 2. Försök parsa input till ett index
        if (int.TryParse(input, out int choice) && choice > 0 && choice <= _categories.Count)
        {
            var selectedCategory = _categories[choice - 1];
            await ShowProducts(selectedCategory);
        }
        else
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            Thread.Sleep(1500);
        }
    }

    private async Task ShowProducts(Kategori selectedCategory)
    {
        bool browsingProducts = true;

        while (browsingProducts)
        {
            Console.Clear();
            Console.WriteLine($"=== Produkter i {selectedCategory.Namn} ===");

            // Hämta produkter som tillhör den valda kategorin
            var products = await context.Produkter
                .Where(p => p.KategoriId == selectedCategory.Id)
                .ToListAsync();

            if (!products.Any())
            {
                Console.WriteLine("Inga produkter hittades i denna kategori.");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {products[i].Namn} ({products[i].Pris:C})");
                }
            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine("0 - Tillbaka till kategorier");
            Console.Write("\nVal: ");

            var input = Console.ReadLine();

            if (input == "0")
            {
                browsingProducts = false; // Bryter loopen och går tillbaka till HandleInput
            }
            //else if (int.TryParse(input, out int choice) && choice > 0 && choice <= products.Count)
            //{
            //    var selectedProduct = products[choice - 1];
            //    // Här kan du gå ännu djupare till en produkt-detaljsida
            //    await ShowProductDetails(selectedProduct);
            //}
        }
    }
}