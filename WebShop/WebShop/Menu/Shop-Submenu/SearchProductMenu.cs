using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService.ShopService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class SearchProductMenu(WebshopDbContext context, ShopSearchProduct searchProduct)
{
    bool _isRunning = true;
   
    //public async Task HandleInput()
    //{
    //    var input = Console.ReadLine().ToUpper();
    //    switch (input)
    //    {
    //        case "1":
    //            await SearchForProduct();
    //            break;
    //        case "2":

    //            break;
    //        case "B":
    //            _isRunning = false;
    //            break;
    //        default:
    //            Console.WriteLine("Ogiltigt val, försök igen.");
    //            Meny.Wait();
    //            break;
    //    }
    //}

    public async Task SearchForProduct()
    {
        Console.Clear();
        Console.Write("=== Sök efter produkt ===\n[B] - Tillbaka\nNamn: ");
        var input = Console.ReadLine().Trim().ToUpper();
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Ingen söksträng angiven.");
            return;
        }
        if (input == "B")
        {
            return;
        }
       
        var products = await context.Produkter
            .Where(p => p.Namn != null && p.Namn.ToLower().Contains(input))
            .Include(p => p.Kategori)
            .ToListAsync();

        InputCheck(input, products);
    }

    private static void InputCheck(string input, List<Produkt> products)
    {
        Console.Clear();
        if (products == null || products.Count == 0)
        {
            Console.WriteLine("Ingen produkt hittades.");
            Meny.Wait();
            return;
        }

        Meny.CreateLines('-', 90);
        foreach (var product in products)
        {
            Console.WriteLine($"Namn: {product.Namn}");
            Console.WriteLine($"Pris: {product.Pris:c}");
            Console.WriteLine($"Antal i lager: {product.LagerAntal}");
            Console.WriteLine($"Kategori: {product.Kategori?.Namn}");
            Console.WriteLine($"Färg: {product.Färg}");
            Console.WriteLine($"Storlek: {product.Storlek}");
            Meny.CreateLines('-', 90);
        }

        Meny.Wait();
    }

    public async Task SearchProductRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            await SearchForProduct();
            break;
            //await HandleInput();
        }
    }
}
