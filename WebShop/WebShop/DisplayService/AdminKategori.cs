using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService;

public class AdminKategori(IKategoriService _kategoriService)
{
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
            ShowListSelection(kategorier, "=== Välj kategori att ta bort ===");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), kategorier.Count, out int kategoriVal))
            {
                Meny.Wait();
                return;
            }
            await ConfirmationDeleteKategori(kategorier, kategoriVal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
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
            ShowListSelection(kategorier, "=== Välj kategori att uppdatera ===");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), kategorier.Count, out int kategoriVal))
            {
                Meny.Wait();
                return;
            }
            string? nyttNamn = GetKategoriName();
            await ConfirmationUpdateKategori(kategorier, kategoriVal, nyttNamn);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    public async Task AddKategoriAsync()
    {
        try
        {
            Console.WriteLine("=== Lägg till kategori ===");
            string? name = GetKategoriName();
            var kategori = new Kategori(name);
            await ConfirmationAddKategori(name, kategori);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
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
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    private async Task ConfirmationAddKategori(string name, Kategori kategori)
    {
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

    private async Task ConfirmationDeleteKategori(List<Kategori> kategorier, int kategoriVal)
    {
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
    private async Task ConfirmationUpdateKategori(List<Kategori> kategorier, int kategoriVal, string nyttNamn)
    {
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
    private static void ShowListSelection(List<Kategori> kategorier, string rubrik)
    {
        Console.WriteLine(rubrik);
        for (int i = 0; i < kategorier.Count; i++)
        {
            Console.WriteLine($"ID: {i + 1} - Namn: {kategorier[i].Namn}");
        }
    }

    private static string GetKategoriName()
    {
        while (true)
        {
            Console.WriteLine("Ange namn: ");
            var name = Console.ReadLine();

            if (!DataValidering.ValidateName(name))
                continue;

            Console.Clear();
            return name;
        }

    }
}

