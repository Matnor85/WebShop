using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;
using System.Linq;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminKampanj(IProduktKampanjService produktKampanjService, IProduktService produktService, IKategoriService kategoriService, ValutaSession valutaSession)
{
    public async Task AddKampanjAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till kampanj ===");

            var kategori = await SelectCategoryAsync();
            if (kategori == null)
            {
                Console.WriteLine("Ingen kategori vald, åtgärden avbruten.");
                Meny.Wait();
                return;
            }

            var produkt = await SelectProductAsync(kategori);
            if (produkt == null)
            {
                Console.WriteLine("Ingen produkt vald, åtgärden avbruten.");
                Meny.Wait();
                return;
            }

            var rabatt = GetRabatt();
            var kampanj = new ProduktKampanj { ProduktId = produkt.Id, Rabatt = rabatt };

            await ConfirmationAddKampanj(produkt, kampanj);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    private async Task<Kategori> SelectCategoryAsync()
    {
        var kategorier = await kategoriService.GetAllAsync();
        if (!DataValidering.ValidateList(kategorier, "Inga kategorier hittades."))
        {
            Meny.Wait();
            return null;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Välj en kategori ===");
            for (int i = 0; i < kategorier.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {kategorier[i].Namn}");
            }
            Console.WriteLine("\n0 - Avbryt");
            Console.Write("Välj kategori: ");

            var input = Console.ReadLine();
            if (input == "0")
            {
                return null;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= kategorier.Count)
            {
                return kategorier[choice - 1];
            }
            else
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
                Meny.Wait();
            }
        }
    }

    private async Task<Produkt> SelectProductAsync(Kategori kategori)
    {
        var allaProdukter = await produktService.GetAllAsync();
        var produkterIKategori = allaProdukter.Where(p => p.KategoriId == kategori.Id).ToList();

        if (!DataValidering.ValidateList(produkterIKategori, $"Inga produkter hittades i kategorin '{kategori.Namn}'."))
        {
            Meny.Wait();
            return null;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Välj en produkt från '{kategori.Namn}' ===");
            for (int i = 0; i < produkterIKategori.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {produkterIKategori[i].Namn}");
            }
            Console.WriteLine("\n0 - Avbryt");
            Console.Write("Välj produkt: ");

            var input = Console.ReadLine();
            if (input == "0")
            {
                return null;
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= produkterIKategori.Count)
            {
                return produkterIKategori[choice - 1];
            }
            else
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
                Meny.Wait();
            }
        }
    }

    public async Task DeleteKampanjAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Ta bort kampanj ===");
            var kampanjer = await produktKampanjService.GetAllAsync();
            if (!DataValidering.ValidateList(kampanjer, "Inga kampanjer hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowKampanjListSelection(kampanjer, "Välj kampanj att ta bort");
            var input = Console.ReadLine();
            if (!DataValidering.ValidateListChoice(input, kampanjer.Count, out int val))
            {
                Meny.Wait();
                return;
            }
            await ConfirmationDeleteKampanj(kampanjer, val);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    public async Task ShowKampanjList()
    {
        Console.Clear();
        var kampanjer = await produktKampanjService.GetAllAsync();
        if (!DataValidering.ValidateList(kampanjer, "Inga kampanjer hittades"))
        {
            Meny.Wait();
            return;
        }
        ShowKampanjListSelection(kampanjer, "=== Kampanjer ===");
        Meny.Wait();
    }
    public async Task UpdateKampanjAsync()
    {
        try
        {
            Console.Clear();
            var kampanjer = await produktKampanjService.GetAllAsync();
            if (!DataValidering.ValidateList(kampanjer, "Inga kampanjer hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowKampanjListSelection(kampanjer, "Välj kampanj att uppdatera");
            var input = Console.ReadLine();
            if (!DataValidering.ValidateListChoice(input, kampanjer.Count, out int val))
            {
                Meny.Wait();
                return;
            }
            var rabatt = GetRabatt();

            if (await HandleZeroRabatt(kampanjer[val - 1], rabatt)) return;
            await ConfirmationUpdateKampanj(kampanjer, val, rabatt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    private async Task<bool> HandleZeroRabatt(ProduktKampanj produktKampanj, decimal rabatt)
    {
        if (rabatt != 0) return false;

        await produktKampanjService.DeleteAsync(produktKampanj.Id);
        Console.Clear();
        Console.WriteLine("Kampanjen har tagits bort eftersom rabatten var 0.");
        Meny.Wait();
        return true;
    }

    private async Task ConfirmationUpdateKampanj(List<ProduktKampanj> kampanjer, int val, decimal rabatt)
    {
        Console.WriteLine($"Vill du uppdatera kampanjen? {kampanjer[val - 1].Produkt.Namn} (J/N)");
        var input = Console.ReadLine()?.Trim().ToLower();
        if (input == "j")
        {
            kampanjer[val - 1].Rabatt = rabatt;
            await produktKampanjService.UpdateAsync(kampanjer[val - 1]);
            Console.Clear();
            Console.WriteLine("Kampanjen har uppdaterats.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kampanjen har inte uppdaterats.");
            Meny.Wait();
        }
    }

    private decimal GetRabatt()
    {
        while (true)
        {
            Console.WriteLine("Ange rabatt (0-100%)");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rabatt))
            {
                Console.WriteLine("Ogiltig inmatning. Försök igen.");
                continue;
            }
            if (KampanjValidering.ValidateRabatt(rabatt))
            {
                return rabatt / 100;
            }
        }
    }

    private async Task ConfirmationDeleteKampanj(List<ProduktKampanj> kampanjer, int val)
    {
        Console.WriteLine($"Vill du ta bort kampanjen på: {kampanjer[val - 1].Produkt.Namn} (J/N)");
        var input = Console.ReadLine()?.Trim().ToLower();
        if (input == "j")
        {
            await produktKampanjService.DeleteAsync(kampanjer[val - 1].Id);
            Console.Clear();
            Console.WriteLine("Kampanjen har tagits bort.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kampanjen har inte tagits bort.");
            Meny.Wait();
        }
    }
    private async Task ConfirmationAddKampanj(Produkt produkt, ProduktKampanj kampanj)
    {
        SummaryKampanj(produkt, kampanj);
        Console.WriteLine("Vill du lägga till kampanjen? (J/N)");
        var input = Console.ReadLine()?.Trim().ToLower();
        if (input == "j")
        {
            await produktKampanjService.AddAsync(kampanj);
            Console.Clear();
            Console.WriteLine("Kampanjen har lagts till.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kampanjen har inte lagts till.");
            Meny.Wait();
        }
    }

    private void SummaryKampanj(Produkt produkt, ProduktKampanj kampanj)
    {
        Console.WriteLine($"Produkt: {produkt.Namn}");
        Console.WriteLine($"Ordinare pris: {valutaSession.FormatPris(produkt.Pris)}");
        Console.WriteLine($"Rabatt: {kampanj.Rabatt * 100}%");
        Console.WriteLine($"Nytt pris: {valutaSession.FormatPris(BeräknaRabattPris(produkt.Pris, kampanj.Rabatt))}");
    }

    private decimal BeräknaRabattPris(decimal pris, decimal rabatt) => pris * (1 - rabatt);

    private void ShowKampanjListSelection(List<ProduktKampanj> kampanjer, string rubrik)
    {
        Console.WriteLine(rubrik);
        for (int i = 0; i < kampanjer.Count; i++)
        {
            Console.WriteLine($"{i + 1} - Produkt: {kampanjer[i].Produkt.Namn}, Rabatt: {kampanjer[i].Rabatt * 100}%");
        }
    }
    private void ShowListSelection(List<Produkt> produkter, string rubrik)
    {
        Console.WriteLine(rubrik);
        for (int i = 0; i < produkter.Count; i++)
        {
            Console.WriteLine($"ID: {i + 1} - Namn: {produkter[i].Namn}");
        }
    }
}