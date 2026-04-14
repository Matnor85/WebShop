using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class BrowseCategoriesMenu(IKategoriService kategoriService)
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
        Console.WriteLine("0 - Tillbaka till huvudmenyn\nVal: ");
    }

    public async Task HandleInput()
    {
        var input = Console.ReadLine();
        if (input == "0")
        {
            _isRunning = false;
            return;
        }
        if (int.TryParse(input, out int choice) && choice > 0 && choice <= _categories.Count)
        {
            var selectedCategory = _categories[choice - 1];
            await ShowProducts(selectedCategory);
        }
        else
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            Console.ReadLine();
        }
    }

    private async Task ShowProducts(Kategori selectedCategory)
    {
        bool browsingProducts = true;
        while (browsingProducts)
        {
            Console.Clear();
            Console.WriteLine($"=== Produkter i {selectedCategory.Namn} ===");
            var kategoriList = kategoriService.GetAllAsync();
            var products = await kategoriService.GetAllAsync()
                .Where(k => k.Id == selectedCategory.Id)
                .Include(k => k.Produkter)
                .SelectMany(k => k.Produkter)
                .OrderBy(p => p.Namn)
                .ThenByDescending(p => p.LagerAntal)
                .ToListAsync();
            if (!products.Any())
            {
                Console.Clear();
                Console.WriteLine("Inga produkter hittades i denna kategori.");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {products[i].Namn} ({products[i].Pris:C})");
                }
                Meny.Wait();
                browsingProducts = false;
            }

            Meny.CreateLines('-', 30);
            Console.WriteLine("0 - Tillbaka till kategorier");
            Console.Write("\nVal: ");

            var input = Console.ReadLine();

            if (input == "0")
            {
                browsingProducts = false;
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