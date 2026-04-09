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

    public AdminKategori(IKategoriService kategoriService)
    {
        _kategoriService = kategoriService;
    }

    public async Task DeleteKategoriAsync()
    {
        try
        {
            Console.WriteLine("=== Ta bort kategori ===");
            var kategorier = await _kategoriService.GetAllAsync();
            if (kategorier == null || kategorier.Count <= 0)
            {
                Console.WriteLine("Inga kategorier hittades.");
                Meny.Wait();
                return;
            }
            Console.WriteLine("Välj en kategori att ta bort med id :");
            for (int i = 0; i < kategorier.Count; i++)
            {
                Console.WriteLine($"ID: {i + 1} - Namn: {kategorier[i].Namn}");
            }
            if (!int.TryParse(Console.ReadLine(), out int kategoriVal) || kategoriVal < 1 || kategoriVal > kategorier.Count)
            {
                throw new ArgumentException("Ogiltigt val av kategori");
            }
            Console.WriteLine($"Vald kategori: {kategorier[kategoriVal - 1].Namn}");
            Console.WriteLine("Är du säker på att du vill ta bort denna kategori? (J/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() == "J")
            {
                await _kategoriService.DeleteAsync(kategorier[kategoriVal - 1].Id);
                Console.Clear();
                Console.WriteLine("Kategorin har tagits bort.");
                Meny.Wait();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Kategorin har inte tagits bort.");
                Meny.Wait();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    public async Task UpdateKategoriAsync()
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

            string? nyttNamn = GetKatogeriName();

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


    public async Task AddKategoriAsync()
    {
        Console.WriteLine("=== Lägg till kategori ===");
        string? name = GetKatogeriName();
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

    private static string GetKatogeriName()
    {
        Console.WriteLine("Ange namn: ");
        var name = Console.ReadLine();
        DataValidering.ValidateName(name);
        Console.Clear();
        return name;
    }

    public async Task ShowKategoriList()
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
}

