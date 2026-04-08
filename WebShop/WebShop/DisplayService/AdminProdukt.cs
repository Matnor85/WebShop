using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
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
                await DeleteProdukt();
                break;
            case "5":
                isRunning = false;
                break;
            default:
                Console.WriteLine("Ogiltigt val, försök igen.");
                break;
        }
    }

    private async Task DeleteProdukt()
    {
        try
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
        catch (ArgumentException ex)
        {

            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    private async Task UpdateProdukt()
    {
        try
        {
            Console.WriteLine("=== Uppdatera produkt ===");
            var produkter = await _produktService.GetAllAsync();
            Console.WriteLine("== Välj produkt att uppdatera ==");
            for (int i = 0; i < produkter.Count; i++)
            {
                Console.WriteLine($"Id: {i + 1} - Namn: {produkter[i].Namn}, Lagersaldo: {produkter[i].LagerAntal}");
            }
            if (!int.TryParse(Console.ReadLine(), out int produktVal) || produktVal < 1 || produktVal > produkter.Count)
            {
                throw new ArgumentException("Ogiltigt val av produkt");
            }
            var leverantörer = await _leverantörService.GetAllAsync();
            var kategorier = await _kategoriService.GetAllAsync();

            var valdLeverantör = leverantörer.FirstOrDefault(l => l.Id == produkter[produktVal - 1].LeverantörId);
            var valdKategori = kategorier.FirstOrDefault(k => k.Id == produkter[produktVal - 1].KategoriId);

            var newProdukt = await ProduktInput();
            Console.WriteLine("Sammanfattning av ändrad produkten:");
            Console.WriteLine($"Namn: {produkter[produktVal - 1].Namn} - Namn: {newProdukt.produkt.Namn}");
            Console.WriteLine($"Beskrivning: {produkter[produktVal - 1].Beskrivning} - Beskrivning: {newProdukt.produkt.Beskrivning}");
            Console.WriteLine($"Pris: {produkter[produktVal - 1].Pris} - Pris: {newProdukt.produkt.Pris}");
            Console.WriteLine($"Färg: {produkter[produktVal - 1].Färg} - Färg: {newProdukt.produkt.Färg}");
            Console.WriteLine($"Storlek: {produkter[produktVal - 1].Storlek} - Storlek: {newProdukt.produkt.Storlek}");
            Console.WriteLine($"Lagerantal: {produkter[produktVal - 1].LagerAntal} - Lagerantal: {newProdukt.produkt.LagerAntal}");
            Console.WriteLine($"Leverantör: {valdLeverantör?.Namn} - Leverantör: {newProdukt.leverantörNamn}");
            Console.WriteLine($"Kategori: {valdKategori?.Namn} - Kategori: {newProdukt.kategoriNamn}");

            Console.WriteLine("Vill du uppdatera produkten? (J/N)");
            var confirm = Console.ReadLine();
            if (confirm.ToUpper() == "J")
            {
                newProdukt.produkt.Id = produkter[produktVal - 1].Id;
                await _produktService.UpdateAsync(newProdukt.produkt);
                Console.Clear();
                Console.WriteLine("Produkten har uppdaterats.");
                Meny.Wait();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Produkten har inte uppdaterats.");
                Meny.Wait();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    private async Task AddProduktAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till produkt === ");
            var produkt = await ProduktInput();

            Console.WriteLine("Sammanfattning av produkten:");
            Console.WriteLine($"Namn: {produkt.produkt.Namn}");
            Console.WriteLine($"Beskrivning: {produkt.produkt.Beskrivning}");
            Console.WriteLine($"Pris: {produkt.produkt.Pris}");
            Console.WriteLine($"Färg: {produkt.produkt.Färg}");
            Console.WriteLine($"Storlek: {produkt.produkt.Storlek}");
            Console.WriteLine($"Lagerantal: {produkt.produkt.LagerAntal}");
            Console.WriteLine($"Leverantör: {produkt.leverantörNamn}");
            Console.WriteLine($"Kategori: {produkt.kategoriNamn}");
            Console.WriteLine("Vill du lägga till produkten? (J/N)");

            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() == "J")
            {
                if (produkt.produkt == null) return;
                await _produktService.AddAsync(produkt.produkt);
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
        try
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
        catch (ArgumentException ex)
        {

            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    private async Task<(Produkt produkt, string leverantörNamn, string kategoriNamn)> ProduktInput()

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
        return (new Produkt(namn, beskrivning, pris, färg, storlek, lagerAntal, leverantörId, kategoriId), leverantörer[leverantörVal - 1].Namn, kategorier[kategoriVal - 1].Namn);
    }
    
}
