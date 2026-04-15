using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.ShopService;

public class ShopSearchProduct(IProduktService produktService, ValutaSession valutaSession)
{
    bool _isRunning = true;

    public async Task SearchForProduct()
    {
        Console.Clear();
        Console.Write("=== Sök efter produkt ===\nSkriv '0' för att gå tillbaka\nNamn: ");
        // ConsoleKeyInfo key; // = Console.ReadKey(true);
        var input = Console.ReadLine()!.Trim().ToLower();
       // key = Console.ReadKey();
        if (input == "0")
        {
         _isRunning = false;
            return;
        }
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Du har inte angett något namn.");
            Meny.Wait();
            return;
        }

        var products = (await produktService.GetAllAsync())
            .Where(p => p.Namn != null && p.Namn.Contains(input!, StringComparison.OrdinalIgnoreCase))
            .ToList();

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
        DisplayProducts(products);

        Meny.Wait();
    }

    private void DisplayProducts(List<Produkt> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Namn: {product.Namn}");
            Console.WriteLine($"Beskrivning: {product.Beskrivning}");
            Console.WriteLine($"Pris: {valutaSession.FormatPris(product.Pris)}");
            Console.WriteLine($"Antal i lager: {product.LagerAntal}");
            Console.WriteLine($"Kategori: {product.Kategori?.Namn}");
            Console.WriteLine($"Leverantör: {product.Leverantör?.Namn}");
            Console.WriteLine($"Färg: {product.Färg}");
            Console.WriteLine($"Storlek: {product.Storlek}");
            Meny.CreateLines('-', 90);
        }
    }

    public async Task SearchProductRun()
    {
        _isRunning = true;
        while (_isRunning)
        {
            await SearchForProduct();
        }
    }
}