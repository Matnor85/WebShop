using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService;

public class AdminKategori
{
    IProduktService _produktService;
    IKategoriService _kategoriService;
    ILeverantörService _leverantörService;
    bool isRunning = true;

    public AdminKategori(IProduktService produktService, IKategoriService kategoriService, ILeverantörService leverantörService)
    {
        _produktService = produktService;
        _kategoriService = kategoriService;
        _leverantörService = leverantörService;
    }

    public void ShowAdminKategoriMenu()
    {
        Console.Clear();
        Console.WriteLine("Hantera kategorier!");
        Console.WriteLine("1 - Visa alla kategorier");
        Console.WriteLine("2 - Skapa ny kategori");
        Console.WriteLine("3 - Uppdatera kategori");
        Console.WriteLine("4 - Ta bort kategori");
        Console.WriteLine("5 - Tillbaka till huvudmenyn");
    }

    public async Task HanteraKategorierAsync()
    {
        Console.Clear();
        ShowAdminKategoriMenu();
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                await ShowKategoriList();
                break;
            case "2":
                await AddKategoriAsync();
                break;
            case "3":
                await UpdateKategoriAsync();
                break;
            case "4":
                await DeleteKategoriAsync();
                break;
            case "5":
                isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }

    private async Task DeleteKategoriAsync()
    {
        throw new NotImplementedException();
    }

    private async Task UpdateKategoriAsync()
    {
        throw new NotImplementedException();
    }

    private async Task AddKategoriAsync()
    {
        Console.WriteLine("=== Lägg till kategori ===");
        Console.WriteLine("Ange namn: ");
        var name = Console.ReadLine();
        DataValidering.ValidateName(name);
        Console.Clear();
        var kategori = new Kategori(name);
        Console.WriteLine($"Är du säker du vill lägga till {name}? (J/N)");
        var confirm = Console.ReadLine();
        if (confirm?.ToUpper() == "J")
        {
            await _kategoriService.AddAsync(kategori);
            Console.Clear();
            Console.WriteLine("Kategorin har lagts till.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kategorin har inte lagts till.");
            Meny.Wait();
        }
    }

    private async Task ShowKategoriList()
    {
        try
        {
            Console.WriteLine("=== Visar kategorier ===");
            var kategorier = await _kategoriService.GetAllAsync();
            if (kategorier == null || kategorier.Count == 0)
            {
                Console.WriteLine("Inga kategorier hittades.");
                Meny.Wait();
                return;
            }
            for (int i = 0; i < kategorier.Count; i++)
            {
                Console.WriteLine($"ID: {i + 1} - Namn: {kategorier[i].Namn}");
            }
            Meny.Wait();

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }
}

