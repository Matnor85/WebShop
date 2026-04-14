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

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class BrowseCategoriesMenu(IKategoriService kategoriService, Kundvagn kundvagn, ShoppingCartMenu shoppingCartMenu)
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
        Console.WriteLine("[B] - Tillbaka till huvudmenyn");
        Console.WriteLine("[K] - Gå till kundvagn");
        Console.Write("Välj kategori: ");
    }

    public async Task HandleInput()
    {
        var input = Console.ReadLine().ToUpper();
        if (input == "B")
        {
            _isRunning = false;
            return;
        }
        else if (input == "K")
        {
            await shoppingCartMenu.ShoppingCartRun();
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
            var products = selectedCategory.Produkter
                .OrderBy(p => p.Namn)
                .ThenByDescending(p => p.LagerAntal)
                .ToList() ?? new List<Produkt>();
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
                //Meny.Wait();
                browsingProducts = false;
            }

            ProductChosies();

            var input = Console.ReadLine().ToUpper();

            if (input == "B")
            {
                browsingProducts = false;
            }
            else if (int.TryParse(input, out int choice) && choice > 0 && choice <= products.Count)
            {
                var selectedProduct = products[choice - 1];

                await AddProductToCart(selectedProduct);
            }
            else if (input == "K")
            {
                await shoppingCartMenu.ShoppingCartRun();
            }
            //else
            //{

            //}
        }
    }

    private static void ProductChosies()
    {
        Meny.CreateLines('-', 30);
        Console.WriteLine("[B] - Tillbaka till kategorier");
        Console.WriteLine("[K] - Gå till kundvagn");
        Console.WriteLine("\nVälj produkt: ");
    }

    private async Task AddProductToCart(Produkt selectedProduct)
    {
        Console.Clear();
        Console.Write($"Ange kvantitet för '{selectedProduct.Namn}' (förvalt antal 1): ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out var amount) || amount <= 0)
        {
            amount = 1;
        }
        Console.Clear();
        kundvagn.AddItem(selectedProduct, amount);
        Console.WriteLine($"{amount} st {selectedProduct.Namn} har lagts till i kundvagnen.");
        Meny.Wait();
    }
   
}