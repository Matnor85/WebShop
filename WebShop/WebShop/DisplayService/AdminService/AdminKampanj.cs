using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminKampanj(IProduktKampanjService produktKampanjService, IProduktService produktService, ValutaSession valutaSession)
{
    public async Task AddKampanjAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till kampanj ===");
            var produkter = await produktService.GetAllAsync();
            ShowListSelection(produkter, "Välj produkt med id");
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), produkter.Count, out int val))
            {
                return;
            }
            var rabatt = GetRabatt();
            var kampanj = new ProduktKampanj { ProduktId = produkter[val - 1].Id, Rabatt = rabatt };
            
            await ConfirmationAddKampanj(produkter[val - 1], kampanj);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
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
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), kampanjer.Count, out int val))
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
            if (!DataValidering.ValidateListChoice(Console.ReadLine(), kampanjer.Count, out int val))
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
        var confirm = Console.ReadLine();
        if (confirm.ToUpper() == "J")
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
        var confirm = Console.ReadLine();
        if (confirm.ToUpper() == "J")
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
        var confirm = Console.ReadLine();
        if (confirm.ToUpper() == "J")
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
