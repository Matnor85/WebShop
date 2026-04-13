using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;
using Webshop.Infrastructure.EF;
using WebShop.Presentation.DisplayService;

namespace WebShop.Presentation.Menu.Shop_Submenu;

public class ManageOrderHistoryMenu
{
    //private readonly WebshopDbContext _context;
    //private readonly ShopManageOrderHistory _shopManageOrderHistory;
    //bool _isRunning = true;
    //public ManageOrderHistoryMenu(WebshopDbContext context, ShopManageOrderHistory orderHistory)
    //{
    //    _context = context;
    //    _shopManageOrderHistory = orderHistory;
    //}


    //public async Task ManageOrderHistory()
    //{
    //    Console.Clear();
    //    Console.WriteLine("=== Användarprofiler och orderhistorik ===");
    //    Console.WriteLine("1 - Ange användarnamn.");
    //    Console.WriteLine("3 - Tillbaka till webb-shopmenyn");

    //}

    //public async Task SearchForUser()
    //{
    //    Console.Clear();
    //    Console.Write("Sök efter (namn/epost):\n");
    //    var input = Console.ReadLine().Trim().ToLower();
    //    if (string.IsNullOrEmpty(input))
    //    {
    //        Console.WriteLine("Ingen söksträng angiven.");
    //        return;
    //    }
    //    var product = await _context.Produkter
    //        .Include(p => p.Namn)
    //        .FirstOrDefaultAsync(p => (p.Namn != null && p.Namn.ToLower().Contains(input)));
    //    InputCheck(input, product);
    //}
    
    //private static void InputCheck(string input, Produkt? product)
    //{
    //    if (product == null)
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Ingen produkt hittades.");
    //        Meny.Wait();
    //    }
    //    else
    //    {
    //        Console.Clear();
    //        Console.WriteLine($"{input} Hittades: \nNamn: {product.Namn} Pris: {product.Pris:c} Antal: {product.LagerAntal}");
    //        Meny.CreateLines('-', 90);
    //        foreach (var item in product.Ordrar)
    //        {
    //            Console.WriteLine($"{item.OrderDatum:yyyy-MM-dd} - {item.TotalPris:c} - Id: {item.Id}");
    //        }
    //        Meny.CreateLines('-', 90);
    //        Meny.Wait();
    //    }
    //}

    //public async Task HandleInput()
    //{
    //    var input = Console.ReadLine();
    //    switch (input)
    //    {
    //        case "1":
    //            await SearchForUser();
    //            break;
    //        case "2":

    //            break;
    //        case "3":
    //            _isRunning = false;
    //            break;
    //        default:
    //            Console.WriteLine("Ogiltigt val, försök igen.");
    //            Meny.Wait();
    //            break;
    //    }
    //}
    //public async Task ManageOrderHistoryRun()
    //{
    //    _isRunning = true;
    //    while (_isRunning)
    //    {
    //        await ManageOrderHistory();
    //        await HandleInput();
    //    }
    //}
}
