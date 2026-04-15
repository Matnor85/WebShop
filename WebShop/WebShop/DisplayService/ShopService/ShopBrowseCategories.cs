using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;
using WebShop.Presentation.Menu.Shop_Submenu;

namespace WebShop.Presentation.DisplayService.ShopService;

public class ShopBrowseCategories(ValutaSession valutaSession, Kundvagn kundvagn, ShoppingCartMenu shoppingCartMenu)
{
    public async Task ShowProducts(Kategori selectedCategory)
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
                    Console.WriteLine($"{i + 1} - {products[i].Namn} ({valutaSession.FormatPris(products[i].Pris)})");
                }
                //Meny.Wait();
                browsingProducts = false;
            }

            ProductChosies();

            var input = Console.ReadLine()?.Trim().ToLower();

            if (input == "esc" || input == "escape" || input == "avbryt")
            {
                browsingProducts = false;
            }
            else if (int.TryParse(input, out int choice) && choice > 0 && choice <= products.Count)
            {
                var selectedProduct = products[choice - 1];

                await AddProductToCart(selectedProduct);
            }
            else if (input == "k")
            {
                await shoppingCartMenu.ShoppingCartRun();
            }

        }
    }

    private static void ProductChosies()
    {
        Meny.CreateLine('-', 30);
        Console.WriteLine("Skriv 'avbryt' för att gå tillbaka till kategorier");
        Console.WriteLine("[K] - Gå till kundvagn");
        Console.WriteLine("\nVälj produkt: ");
    }

    private async Task AddProductToCart(Produkt selectedProduct)
    {
        Console.Clear();
        Console.WriteLine($"Beskrivning: {selectedProduct.Beskrivning}");
        Console.Write($"Ange kvantitet för '{selectedProduct.Namn}' (förvalt antal 1): ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out var amount) || amount <= 0)
        {
            amount = 1;
        }
        Console.Clear();
        if (ConfirmToAdd(amount, selectedProduct))
        {
            kundvagn.AddItem(selectedProduct, amount);
        }
    }

    private bool ConfirmToAdd(int amount, Produkt selectedProduct)
    {
        Console.WriteLine("Är du säker på att du vill lägga till denna produkt i kundvagnen? (J/N)");
        var input = Console.ReadLine()?.Trim().ToLower();
        if (input == "j")
        {
            Console.Clear();
            Console.WriteLine($"{amount} st {selectedProduct.Namn} har lagts till i kundvagnen.");
            Meny.Wait();
            return true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Produkt inte tillagd i kundvagnen.");
            Meny.Wait();
            return false;    
        }
    }
}
