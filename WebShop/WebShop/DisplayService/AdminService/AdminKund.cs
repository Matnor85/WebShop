using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.Helpers;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.AdminService;

public class AdminKund(IKundService kundService, KundInputHelper kundInputHelper    )
{
    public async Task AddKundAsync()
    {
        try
        {
            Console.WriteLine("=== Lägg till kund ===");
            var kund =  kundInputHelper.KundInput();
            if (kund == null)
            {
                Console.WriteLine("Kunden kunde inte skapas.");
                return;
            }

            Console.WriteLine("Sammanfattning av kund du vill lägga till:");
            Console.WriteLine(kund);

            await ConfirmationAddKund(kund);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message}\n {ex.StackTrace}");
        }
    }

    public async Task DeleteKundAsync()
    {
        try
        {
            Console.WriteLine("=== Ta bort kund ===");
            var kunder = await kundService.GetAllAsync();
            if (!DataValidering.ValidateList(kunder, "Inga kunder hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowListSelection(kunder, "=== Välj kund att ta bort ===");

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (!DataValidering.ValidateListChoice(key.KeyChar.ToString(), kunder.Count, out int choice))
            {
                Meny.Wait();
                return;
            }
            await ConfirmationDeleteKund(kunder, choice);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }
    private static void ShowListSelection(List<Kund> kunder, string rubrik)
    {
        Console.WriteLine(rubrik);
        for (int i = 0; i < kunder.Count; i++)
            Console.WriteLine($"{i + 1}. Namn: {kunder[i].Namn}, Email: {kunder[i].Epost}, Adress: {kunder[i].Adress}");
    }

    public async Task ShowKundList()
    {
        try
        {
            Console.WriteLine("=== Visar kunder ===");
            var kunder = await kundService.GetAllAsync();
            if (!DataValidering.ValidateList(kunder, "Inga kunder hittades"))
            {
                Meny.Wait();
                return;
            }
            foreach (var kund in kunder)
            {
                Console.WriteLine($"Namn: {kund.Namn}, Email: {kund.Epost}, Adress: {kund.Adress}");
            }
             Meny.Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }

    public async Task UpdateKundAsync()
    {
        try
        {
            Console.WriteLine("=== Uppdatera kund ===");
            var kunder = await kundService.GetAllAsync();
            if (!DataValidering.ValidateList(kunder, "Inga kunder hittades"))
            {
                Meny.Wait();
                return;
            }
            ShowListSelection(kunder, "=== Välj kund att uppdatera ===");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (!DataValidering.ValidateListChoice(key.KeyChar.ToString(), kunder.Count, out int choice))
            {
                Meny.Wait();
                return;
            }
            var newKund =  kundInputHelper.KundInput();
            UpdateSummary(kunder, choice, newKund);
            await ConfirmationUpdateKund(kunder, choice, newKund);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel: {ex.Message} \n {ex.StackTrace}");
        }
    }
    private async Task ConfirmationDeleteKund(List<Kund> kunder, int choice)
    {
        Console.WriteLine($"Vill du ta bort kunden {kunder[choice - 1].Namn} (J/N)?");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.J)
        {
            await kundService.DeleteAsync(kunder[choice - 1].Id);
            Console.Clear();
            Console.WriteLine("Kunden har tagits bort.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kunden har inte tagits bort.");
            Meny.Wait();
        }
    }
    private async Task ConfirmationUpdateKund(List<Kund> kunder, int choice, Kund newKund)
    {
        Console.WriteLine("Vill du uppdatera kunden (J/N)?");
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.J)
        {
            newKund.Id = kunder[choice - 1].Id;
            await kundService.UpdateAsync(newKund);
            Console.Clear();
            Console.WriteLine("Kunden har uppdaterats.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kunden har inte uppdaterats.");
            Meny.Wait();
        }
    }

    private async Task ConfirmationAddKund(Kund kund)
    {
        Console.WriteLine("Vill du lägga till kunden (J/N)");

        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.J)
        {
            await kundService.AddAsync(kund);
            Console.Clear();
            Console.WriteLine("Kunden har lagts till.");
            Meny.Wait();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Kunden har inte lagts till.");
            Meny.Wait();
        }
    }
    private static void UpdateSummary(List<Kund> kunder, int choice, Kund newKund)
    {
        Console.WriteLine("Sammanfattning av ändrad kund: ");
        Console.WriteLine($"Namn: {kunder[choice - 1].Namn} - {newKund.Namn}");
        Console.WriteLine($"Email: {kunder[choice - 1].Epost} - {newKund.Epost}");
        Console.WriteLine($"Adress: {kunder[choice - 1].Adress} - {newKund.Adress}");
        Console.WriteLine($"Stad: {kunder[choice - 1].Stad} - {newKund.Stad}");
        Console.WriteLine($"Postnummer: {kunder[choice - 1].Postnummer} - {newKund.Postnummer}");
        Console.WriteLine($"Telefonnummer: {kunder[choice - 1].MobilNummer} - {newKund.MobilNummer}");
    }
}
