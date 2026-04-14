using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using Webshop.Domain.Enums;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminProdukt(IProduktService produktService, IKategoriService kategoriService, ILeverantörService leverantörService)
{
    public async Task DeleteProdukt()
    {
        try
        {
            Console.WriteLine("=== Ta bort produkt ===");
            var produkter = await produktService.GetAllAsync();

            if (!DataValidering.ValidateList(produkter, "Inga produkter hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowProduktListForSelection(produkter, "=== Välj vilken produkt du vill ta bort ===");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), produkter.Count, out int produktVal))
            {
                Meny.Wait();
                return;
            }
            await ConfirmDeleteProdukt(produkter, produktVal);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
        
    }
    public async Task UpdateProdukt()
    {
        try
        {
            Console.WriteLine("=== Uppdatera produkt ===");
            var produkter = await produktService.GetAllAsync();
            if (!DataValidering.ValidateList(produkter, "Inga produkter hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowProduktListForSelection(produkter, "=== Välj vilken produkt du vill uppdatera ===");

            if (!DataValidering.ValidateListChoice(Console.ReadLine(), produkter.Count, out int produktVal))
            {
                Meny.Wait();
                return;
            }
            var newProdukt = await ProduktInput();
            UpdateSummary(produkter, produktVal, newProdukt);

            await ConfirmUpdateProdukt(produkter, produktVal, newProdukt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }
    public async Task AddProduktAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till produkt === ");
            var produktResult = await ProduktInput();
            Console.WriteLine(produktResult.produkt);
            Console.WriteLine($"Leverantör: {produktResult.leverantörNamn}\nKategori: {produktResult.kategoriNamn}");

            await ConfirmAddProdukt(produktResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    public async Task ShowProduktList()
    {
        Console.WriteLine("=== Visar produkter ===");
        var produktList = await produktService.GetAllAsync();
        if (!DataValidering.ValidateList(produktList, "Inga produkter hittades"))
        {
            Meny.Wait();
            return;
        }
        foreach (var p in produktList)
            Console.WriteLine($"Namn: {p.Namn}, Lagersaldo: {p.LagerAntal}");

        Meny.Wait();
    }
    private async Task ConfirmAddProdukt((Produkt produkt, string leverantörNamn, string kategoriNamn) produktResult)
    {
        Console.WriteLine("Vill du lägga till produkten? (J/N)");
        var confirm = Console.ReadLine();
        if (confirm?.ToUpper() == "J")
        {
            await produktService.AddAsync(produktResult.produkt);
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

    private async Task ConfirmDeleteProdukt(List<Produkt> produkter, int produktVal)
    {
        Console.WriteLine($"Vill du ta bort produkten? (J/N) {produkter[produktVal - 1].Namn}");
        var confirm = Console.ReadLine();
        if (confirm?.ToUpper() == "J")
        {
            await produktService.DeleteAsync(produkter[produktVal - 1].Id);
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
    private void UpdateSummary(List<Produkt> produkter, int produktVal, (Produkt produkt, string leverantörNamn, string kategoriNamn) newProdukt)
    {
        Console.WriteLine("Sammanfattning av ändrad produkten:");
        Console.WriteLine($"Namn: {produkter[produktVal - 1].Namn} - Namn: {newProdukt.produkt.Namn}");
        Console.WriteLine($"Beskrivning: {produkter[produktVal - 1].Beskrivning} - Beskrivning: {newProdukt.produkt.Beskrivning}");
        Console.WriteLine($"Pris: {produkter[produktVal - 1].Pris} - Pris: {newProdukt.produkt.Pris}");
        Console.WriteLine($"Färg: {produkter[produktVal - 1].Färg} - Färg: {newProdukt.produkt.Färg}");
        Console.WriteLine($"Storlek: {produkter[produktVal - 1].Storlek} - Storlek: {newProdukt.produkt.Storlek}");
        Console.WriteLine($"Lagerantal: {produkter[produktVal - 1].LagerAntal} - Lagerantal: {newProdukt.produkt.LagerAntal}");
        Console.WriteLine($"Leverantör: {produkter[produktVal - 1].Leverantör.Namn} - Leverantör: {newProdukt.leverantörNamn}");
        Console.WriteLine($"Kategori: {produkter[produktVal - 1].Kategori.Namn} - Kategori: {newProdukt.kategoriNamn}");
    }
    private static void ShowProduktListForSelection(List<Produkt> produkter, string rubrik)
    {
        Console.WriteLine(rubrik);
        for (int i = 0; i < produkter.Count; i++)
            Console.WriteLine($"{i + 1} - Namn: {produkter[i].Namn}, Lagersaldo: {produkter[i].LagerAntal}");
    }
    private async Task ConfirmUpdateProdukt(List<Produkt> produkter, int produktVal, (Produkt produkt, string leverantörNamn, string kategoriNamn) newProdukt)
    {
        Console.WriteLine("Vill du uppdatera produkten? (J/N)");
        var confirm = Console.ReadLine();
        if (confirm?.ToUpper() == "J")
        {
            newProdukt.produkt.Id = produkter[produktVal - 1].Id;
            await produktService.UpdateAsync(newProdukt.produkt);
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
    //Skapa factory metod för att samla in all input för en produkt, inklusive val av leverantör och kategori, och returnera en "tuple" med produkten och de valda namnen för leverantör och kategori.
    public async Task<(Produkt produkt, string leverantörNamn, string kategoriNamn)> ProduktInput()
    {
        var namn = GetName();
        var beskrivning = GetDescription();
        var pris = GetPrice();
        var färg = GetFärg();
        var storlek = GetStorlek();
        var lagerAntal = GetLagerAntal();
        var (leverantörId, leverantörNamn) = await GetLeverantörAsync();
        var (kategoriId, kategoriNamn) = await GetKategoriAsync();

        return (new Produkt(namn, beskrivning, pris, färg, storlek, lagerAntal, leverantörId, kategoriId), leverantörNamn, kategoriNamn);
    }

    public string GetName()
    {
        while (true)
        {
            Console.WriteLine("Ange namn");
            var namn = Console.ReadLine();
            
            if(!DataValidering.ValidateName(namn))
                continue;
                
            Console.Clear();
            return namn;
            
            
        }
    }

    public string GetDescription()
    {
        while (true)
        {
            Console.WriteLine("Ange Beskrivning");
            var beskrivning = Console.ReadLine();
            if (!ProduktValidering.ValidateDescription(beskrivning))
                continue;
            
            Console.Clear();
            return beskrivning;
            
        }
    }

    public decimal GetPrice()
    {
        while (true)
        {
            Console.WriteLine("Ange Pris");
            var input = Console.ReadLine();
            if (!decimal.TryParse(input, out decimal pris))
            {
                Console.WriteLine("Ogiltigt prisformat");
                continue;
            }
            
            if(!DataValidering.ValidatePrice(pris))
                continue;

            Console.Clear();
            return pris;
        }
    }

    public Färg GetFärg()
    {
        while (true)
        {
            Console.WriteLine("Ange färg: ");
            foreach (var color in Enum.GetValues(typeof(Färg)).Cast<Färg>().Where(f => f != Färg.Okänd))
            {
                Console.WriteLine($"{(int)color} - {color}");
            }
            var input = Console.ReadLine();

            if (!ProduktValidering.ValidateFärg(input, out Färg färg))
                continue;

            Console.Clear();
            return färg;
        }
    }

    public Storlek GetStorlek()
    {
        while (true)
        {
            Console.WriteLine("Ange Storlek: ");
            foreach (var s in Enum.GetValues(typeof(Storlek)).Cast<Storlek>().Where(s => s != Storlek.Okänd))
            {
                Console.WriteLine($"{(int)s} - {s}");
            }
            var input = Console.ReadLine();

            if (!ProduktValidering.ValidateStorlek(input, out Storlek storlek))
                continue;

            Console.Clear();
            return storlek;
        }
    }
    public int GetLagerAntal()
    {
        while (true)
        {
            Console.WriteLine("Ange Lagerantal: ");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int lagerAntal))
            {
                Console.WriteLine("Ogiltigt lagerantal");
                continue;
            }

            if (!ProduktValidering.ValidateStock(lagerAntal))
                continue;

            Console.Clear();
            return lagerAntal;
            
        }
    }

    public async Task<(Guid id, string namn)> GetLeverantörAsync()
    {
        
        var leverantörer = await leverantörService.GetAllAsync();
        while (true)
        {
            Console.WriteLine("Ange leverantör:");
            for (int i = 0; i < leverantörer.Count; i++)
                Console.WriteLine($"{i + 1} - {leverantörer[i].Namn}");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), leverantörer.Count, out int val))
                continue;
            
            Console.Clear();
            return (leverantörer[val - 1].Id, leverantörer[val - 1].Namn);
        }
    }

    public async Task<(Guid id, string namn)> GetKategoriAsync()
    {
        var kategorier = await kategoriService.GetAllAsync();
        while (true)
        {
            Console.WriteLine("Ange kategori:");
            for (int i = 0; i < kategorier.Count; i++)
                Console.WriteLine($"{i + 1} - {kategorier[i].Namn}");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), kategorier.Count, out int val))
                continue;
            
            Console.Clear();
            return (kategorier[val - 1].Id, kategorier[val - 1].Namn);
        }
    }
}
