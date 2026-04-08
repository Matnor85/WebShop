using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Services;
using Webshop.Domain.Enums;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService;

public class AdminProdukt
{
    static ProduktService _produktService;
    static KategoriService _kategoriService;
    static KundService _kundService;
    static LeverantörService _leverantörService;
    static FraktOmbudService _fraktService;
    static OrderService _orderService;
    static ProduktOrderService _produktOrderService;
    static bool isRunning = true;
    public AdminProdukt()
    {
        
    }
    public AdminProdukt(ProduktService produktService, KategoriService kategoriService, KundService kundService, LeverantörService leverantörService, FraktOmbudService fraktService, OrderService orderService, ProduktOrderService produktOrderService)
    {
        _produktService = produktService;
        _kategoriService = kategoriService;
        _kundService = kundService;
        _leverantörService = leverantörService;
        _fraktService = fraktService;
        _orderService = orderService;
        _produktOrderService = produktOrderService;
    }
    public static void ShowProduktMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera produkter");
        Console.WriteLine("1 - Visa alla produkter");
        Console.WriteLine("2 - Lägg till produkt");
        Console.WriteLine("3 - Uppdatera produkt");
        Console.WriteLine("4 - Ta bort produkt");
        Console.WriteLine("5 - Tillbaka till adminmenyn");
    }
    public static void HanteraProdukter()
    {
        Console.Clear();
        ShowProduktMenu();
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                ShowProduktList();
                break;
            case "2":
                AddProdukt();
                break;
            case "3":
                UpdateProdukt();
                break;
            case "4":
                DeleteProdukt();
                break;
            case "5":
                isRunning = false;
                // Tillbaka till adminmenyn
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }

    private static void DeleteProdukt()
    {
        throw new NotImplementedException();
    }

    private static void UpdateProdukt()
    {
        throw new NotImplementedException();
    }

    private static void AddProdukt()
    {
        Console.Clear();
        Console.WriteLine("=== Lägg till produkt === ");
        try
        {
            Console.WriteLine("Ange namn");
            var namn = Console.ReadLine();
            DataValidering.ValidateName(namn);
            Console.Clear();

            Console.WriteLine("Ange Beskrivning");
            var beskrivning = Console.ReadLine();
            ProduktValidering.ValidateDescription(beskrivning);
            Console.Clear();

            Console.WriteLine("Ange Pris");
            if (!decimal.TryParse(Console.ReadLine(), out decimal pris))
            {
                Console.WriteLine("Ogiltigt prisformat");
                return;
            }
            DataValidering.ValidatePrice(pris);
            Console.Clear();

            Console.WriteLine("Ange färg: ");
            foreach (var color in Enum.GetValues(typeof(Färg)).Cast<Färg>().Where(f => f != Färg.Okänd))
            {
                Console.WriteLine($"{(int)color} - {color}");
            }
            var färgInput = Console.ReadLine();
            ProduktValidering.ValidateFärg(färgInput, out Färg färg);
            Console.Clear();

            Console.WriteLine("Ange Storlek: ");
            foreach (var s in Enum.GetValues(typeof(Storlek)).Cast<Storlek>().Where(s => s != Storlek.Okänd))
            {
                Console.WriteLine($"{(int)s} - {s}");
            }
            var storlekInput = Console.ReadLine();
            ProduktValidering.ValidateStorlek(storlekInput, out Storlek storlek);
            Console.Clear();

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }

    }

    private static async Task ShowProduktList()
    {
        Console.WriteLine("=== Visar produkter ===");
        var produktList = await _produktService.GetAllAsync();
        if (produktList == null || produktList.Count <= 0)
        {
            Console.WriteLine("inga produkter hittades.");
            return;
        }
        foreach (var p in produktList)
        {
            Console.WriteLine($"Id: {p.Id}, Namn: {p.Namn}, Lagersaldo: {p.LagerAntal} ");
        }
        Meny.Wait();
    }

    public static void ProduktMenuRun()
    {
        
        while (isRunning)
        {
            HanteraProdukter();
        }
    }
}
