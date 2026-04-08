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
                ShowProduktList();
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

    private void DeleteProdukt()
    {
        throw new NotImplementedException();
    }

    private void UpdateProdukt()
    {
        throw new NotImplementedException();
    }

    private async Task AddProduktAsync()
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

            Console.WriteLine("Ange Lagerantal: ");
            if (!int.TryParse(Console.ReadLine(), out int lagerAntal))
            {
                Console.WriteLine("Ogiltigt lagerantal");
                return;
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
                Console.WriteLine("Ogiltigt val av leverantör");
                return;
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
                Console.WriteLine("Ogiltigt val av kategori");
                return;
            }
            var kategoriId = kategorier[kategoriVal - 1].Id;
            Console.Clear();

            Console.WriteLine("Sammanfattning av produkten:");
            Console.WriteLine($"Namn: {namn}");
            Console.WriteLine($"Beskrivning: {beskrivning}");
            Console.WriteLine($"Pris: {pris}");
            Console.WriteLine($"Färg: {färg}");
            Console.WriteLine($"Storlek: {storlek}");
            Console.WriteLine($"Lagerantal: {lagerAntal}");
            Console.WriteLine($"Leverantör: {leverantörer[leverantörVal - 1].Namn}");
            Console.WriteLine($"Kategori: {kategorier[kategoriVal - 1].Namn}");
            Console.WriteLine("Vill du lägga till produkten? (J/N)");
            
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() == "J")
            {
                await _produktService.AddAsync(new Produkt
                {
                    Namn = namn,
                    Beskrivning = beskrivning,
                    Pris = pris,
                    Färg = färg,
                    Storlek = storlek,
                    LagerAntal = lagerAntal,
                    LeverantörId = leverantörId,
                    KategoriId = kategoriId
                });
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
            return;
        }
        foreach (var p in produktList)
        {
            Console.WriteLine($"Id: {p.Id}, Namn: {p.Namn}, Lagersaldo: {p.LagerAntal} ");
        }
        Meny.Wait();
    }

    public void ProduktMenuRun()
    {
        
        while (isRunning)
        {
            HanteraProdukterAsync();
        }
    }
}
