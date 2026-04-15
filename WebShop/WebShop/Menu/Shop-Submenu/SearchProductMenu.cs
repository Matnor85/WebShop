using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService.ShopService;
using WebShop.Presentation.DisplayService.ValutaApi;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class SearchProductMenu(WebshopDbContext context, ShopSearchProduct searchProduct, ValutaSession valutaSession)
{
    bool _isRunning = true;
   
    public async Task SearchForProduct()
    {
        Console.Clear();
        Console.Write("=== Sök efter produkt ===\n[Esc] - Tillbaka\nNamn: ");
        var input = Console.ReadLine()!.Trim();
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Ingen söksträng angiven.");
            return;
        }
        if (key.Key == ConsoleKey.Escape)
        {
            return;
        }
       
        var products = await context.Produkter
            .Where(p => p.Namn != null && p.Namn.Contains(input))
            .Include(p => p.Kategori)
            .ToListAsync();

        InputCheck(input, products);
    }

    private void InputCheck(string input, List<Produkt> products)
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
            Console.WriteLine($"Pris: {valutaSession.FormatPris(product.Pris)}");
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
