using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Application.Services;
using Webshop.Domain.Entitites;
using Webshop.Domain.Enums;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService;

public class AdminProdukt
{
    IProduktService _produktService;
    IKategoriService _kategoriService;
    ILeverantörService _leverantörService;
    bool isRunning = true;
    
    public AdminProdukt(IProduktService produktService, IKategoriService kategoriService, ILeverantörService leverantörService)
    {
        _produktService = produktService;
        _kategoriService = kategoriService;
        _leverantörService = leverantörService;
    }

    public void ShowProduktMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera produkter");
        Console.WriteLine("1 - Visa alla produkter");
        Console.WriteLine("2 - Lägg till produkt");
        Console.WriteLine("3 - Uppdatera produkt");
        Console.WriteLine("4 - Ta bort produkt");
        Console.WriteLine("5 - Tillbaka till adminmenyn");
    }
    public async Task HanteraProdukterAsync()
    {
        Console.Clear();
        ShowProduktMenu();
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                await ShowProduktList();
                break;
            case "2":
                await AddProduktAsync();
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

    private async Task DeleteProdukt()
    {
        Console.WriteLine("=== Ta bort produkt ===");
        var produkter = await _produktService.GetAllAsync();
            if (produkter == null || produkter.Count <= 0) return;
        Console.WriteLine("== Välj vilket produkt du vill ta bort ==");
        for (int i = 0; i < produkter.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {produkter[i].Namn}, Lagersaldo: {produkter[i].LagerAntal}");
        }
        if (!int.TryParse(Console.ReadLine(), out int produktVal) || produktVal < 1 || produktVal > produkter.Count)
        {
            throw new ArgumentException("Ogiltigt val av produkt");
        }
        Console.WriteLine($"Vill du ta bort produkten? (J/N) {produkter[produktVal - 1].Namn}");
        var confirm = Console.ReadLine();
        if (confirm.ToUpper() == "J")
        {
            await _produktService.DeleteAsync(produkter[produktVal - 1].Id);
            Console.Clear();
            Console.WriteLine("Produkten har tagits bort.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Produkten har inte tagits bort.");
            Meny.Wait();
        }
    }

    private void UpdateProdukt()
    {
        Console.WriteLine("=== Uppdatera produkt ===");
    }

    private async Task AddProduktAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till produkt === ");
            var produkt = await ProduktInput();

            Console.WriteLine("Sammanfattning av produkten:");
            Console.WriteLine($"Namn: {produkt.Namn}");
            Console.WriteLine($"Beskrivning: {produkt.Beskrivning}");
            Console.WriteLine($"Pris: {produkt.Pris}");
            Console.WriteLine($"Färg: {produkt.Färg}");
            Console.WriteLine($"Storlek: {produkt.Storlek}");
            Console.WriteLine($"Lagerantal: {produkt.LagerAntal}");
            Console.WriteLine($"Leverantör: {produkt.LeverantörId}");
            Console.WriteLine($"Kategori: {produkt.KategoriId}");
            Console.WriteLine("Vill du lägga till produkten? (J/N)");
            
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() == "J")
            {
                if (produkt == null) return;
                await _produktService.AddAsync(produkt);
                Console.Clear();
                Console.WriteLine("Produkten har lagts till.");
                Meny.Wait();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Produkten har inte lagts till.");
                Meny.Wait();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }

    }

    private async Task ShowProduktList()
    {
        Console.WriteLine("=== Visar produkter ===");
        var produktList = await _produktService.GetAllAsync();
        if (produktList == null || produktList.Count <= 0)
        {
            Console.WriteLine("inga produkter hittades.");
            Meny.Wait();
            return;
        }
        foreach (var p in produktList)
        {
            Console.WriteLine($"Id: {p.Id}, Namn: {p.Namn}, Lagersaldo: {p.LagerAntal} ");
        }
        Meny.Wait();
    }

    private async Task<Produkt?> ProduktInput()
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
            throw new ArgumentException("Ogiltigt prisformat");
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

        Console.WriteLine("Ange Lagerantal: ");
        if (!int.TryParse(Console.ReadLine(), out int lagerAntal))
        {
            throw new ArgumentException("Ogiltigt lagerantal");
        }
        ProduktValidering.ValidateStock(lagerAntal);
        Console.Clear();

        Console.WriteLine("Ange Leverantör: ");
        var leverantörer = await _leverantörService.GetAllAsync();
        for (int i = 0; i < leverantörer.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {leverantörer[i].Namn}");
        }
        if (!int.TryParse(Console.ReadLine(), out int leverantörVal) || leverantörVal < 1 || leverantörVal > leverantörer.Count)
        {
            throw new ArgumentException("Ogiltigt val av leverantör");
        }
        var leverantörId = leverantörer[leverantörVal - 1].Id;
        Console.Clear();

        Console.WriteLine("Ange Kategori: ");
        var kategorier = await _kategoriService.GetAllAsync();
        for (int i = 0; i < kategorier.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {kategorier[i].Namn}");
        }
        if (!int.TryParse(Console.ReadLine(), out int kategoriVal) || kategoriVal < 1 || kategoriVal > kategorier.Count)
        {
            throw new ArgumentException("Ogiltigt val av kategori");
        }
        var kategoriId = kategorier[kategoriVal - 1].Id;
        Console.Clear();
        return new Produkt(namn, beskrivning, pris, färg, storlek, lagerAntal, leverantörId, kategoriId);
    }
    public void ProduktMenuRun()
    {
        
        while (isRunning)
        {
            HanteraProdukterAsync();
        }
    }
}
