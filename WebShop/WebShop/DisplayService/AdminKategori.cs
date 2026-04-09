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
    IKategoriService _kategoriService;
    bool _isRunning = true;

    public AdminKategori(IKategoriService kategoriService)
    {
        _kategoriService = kategoriService;
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
                _isRunning = false;
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
        try
        {
            Console.WriteLine("=== Uppdatera kategori ===");
            var kategorier = await _kategoriService.GetAllAsync();
            if (kategorier == null || kategorier.Count <= 0)
            {
                Console.WriteLine("Inga kategorier hittades.");
                Meny.Wait();
                return;
            }
            for (int i = 0; i < kategorier.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {kategorier[i].Namn}");
            }
            if (!int.TryParse(Console.ReadLine(), out int kategoriVal) || kategoriVal < 1 || kategoriVal > kategorier.Count)
            {
                throw new ArgumentException("Ogiltigt val av kategori");
            }

            Console.WriteLine("Ange nytt namn:");
            var nyttNamn = Console.ReadLine();
            DataValidering.ValidateName(nyttNamn);

            Console.WriteLine("Sammanfattning av ändrad kategori:");
            Console.WriteLine($"Namn: {kategorier[kategoriVal - 1].Namn} - {nyttNamn}");
            Console.WriteLine("Vill du uppdatera denna kategori? (J/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() == "J")
            {
                kategorier[kategoriVal - 1].Namn = nyttNamn;
                await _kategoriService.UpdateAsync(kategorier[kategoriVal - 1]);
                Console.Clear();
                Console.WriteLine("Kategorin har uppdaterats.");
                Meny.Wait();
            }
            else
            {
                Console.WriteLine("Kategorin har inte uppdaterats.");
                Meny.Wait();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
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
            if (kategorier == null || kategorier.Count <= 0)
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

    public async Task KategoriMenuRunAsync()
    {
        while (_isRunning)
        {
            await HanteraKategorierAsync();
        }
    }
}

