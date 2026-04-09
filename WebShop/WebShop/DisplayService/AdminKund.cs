using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService;

public class AdminKund
{
    IKundService _kundService;
    public AdminKund(IKundService kundService)
    {
         _kundService = kundService;
    }
    public async Task AddKundAsync()
    {
        try
        {
            Console.WriteLine("=== Lägg till kund ===");
            var kund = await KundInput();
            if (kund == null)
            {
                Console.WriteLine("Kunden kunde inte skapas.");
                return;
            }

            Console.WriteLine("Sammanfattning av kund du vill lägga till:");
            Console.WriteLine($"Namn: {kund.Namn}");
            Console.WriteLine($"Adress: {kund.Adress}");
            Console.WriteLine($"Stad: {kund.Stad}");
            Console.WriteLine($"Postnummer: {kund.Postnummer}");
            Console.WriteLine($"Telefonnummer: {kund.MobilNummer}");
            Console.WriteLine($"E-post: {kund.Epost}");
            Console.WriteLine("Vill du lägga till kunden (J/N)");

            var confirm = Console.ReadLine();
            if (confirm.ToUpper() == "J")
            {
                await _kundService.AddAsync(kund);
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
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    public async Task DeleteKundAsync()
    {
        try
        {
            Console.WriteLine("=== Ta bort kund ===");
            var kunder = await _kundService.GetAllAsync();
            if (kunder == null || kunder.Count <= 0)
            {
                Console.WriteLine("Inga kunder hittades.");
                Meny.Wait();
                return;
            }
            Console.WriteLine("Välj kund att ta bort:");
            for (int i = 0; i < kunder.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Namn: {kunder[i].Namn}, Email: {kunder[i].Epost}, Adress: {kunder[i].Adress}");
            }
            if(!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > kunder.Count)
            {
                throw new ArgumentException("Ogiltigt val. Vänligen ange ett nummer från listan.");
            }
            Console.WriteLine($"Vill du ta bort kunden {kunder[choice - 1].Namn} (J/N)?");
            var confirm = Console.ReadLine();
            if (confirm.ToUpper() == "J")
            {
                await _kundService.DeleteAsync(kunder[choice - 1].Id);
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
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    public async Task ShowKundList()
    {
        try
        {
            Console.WriteLine("=== Visar kunder ===");
            var kunder = await _kundService.GetAllAsync();
            if (kunder != null && kunder.Count > 0)
            {
                foreach (var kund in kunder)
                {
                    Console.WriteLine($"Namn: {kund.Namn}, Email: {kund.Epost}, Adress: {kund.Adress}");
                }
            }
            else
            {
                Console.WriteLine("Inga kunder att visa.");
            }
             Meny.Wait();

            }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }

    public async Task UpdateKundAsync()
    {
        try
        {
            Console.WriteLine("=== Uppdatera kund ===");
            var kunder = await _kundService.GetAllAsync();
            if (kunder == null || kunder.Count <= 0)
            {
                Console.WriteLine("Inga kunder hittades.");
                Meny.Wait();
                return;
            }
            Console.WriteLine("Välj kund att uppdatera:");
            for (int i = 0; i < kunder.Count; i++)
            {
                Console.WriteLine($"Id: {i + 1}, Namn: {kunder[i].Namn}, Email: {kunder[i].Epost}, Adress: {kunder[i].Adress}");
            }
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > kunder.Count)
            {
                throw new ArgumentException("Ogiltigt val. Vänligen ange ett nummer från listan.");
            }
            var newKund = await KundInput();
            Console.WriteLine("Sammanfattning av ändrad kund: ");
            Console.WriteLine($"Namn: {kunder[choice - 1].Namn} - {newKund.Namn}");
            Console.WriteLine($"Email: {kunder[choice - 1].Epost} - {newKund.Epost}");
            Console.WriteLine($"Adress: {kunder[choice - 1].Adress} - {newKund.Adress}");
            Console.WriteLine($"Stad: {kunder[choice - 1].Stad} - {newKund.Stad}");
            Console.WriteLine($"Postnummer: {kunder[choice - 1].Postnummer} - {newKund.Postnummer}");
            Console.WriteLine($"Telefonnummer: {kunder[choice - 1].MobilNummer} - {newKund.MobilNummer}");
            Console.WriteLine("Vill du uppdatera kunden (J/N)?");
            
            var confirm = Console.ReadLine();
            if (confirm.ToUpper() == "J")
            {
                newKund.Id = kunder[choice - 1].Id;
                await _kundService.UpdateAsync(newKund);
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
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Fel: {ex.Message}");
        }
    }


    public async Task<Kund> KundInput()
    {
        var namn = GetKundName();
        var adress = GetAddress();
        var city = GetCity();
        var zipCode = GetZipCode();
        var phoneNumber = GetPhoneNumber();
        var email = GetEmail();

        return new Kund(namn, adress, city, zipCode, phoneNumber, email);
        
    }
    public string GetKundName()
    {
        while (true)
        {
            Console.WriteLine("Ange namn: ");
            var namn = Console.ReadLine();
            try
            {
                DataValidering.ValidateName(namn);
                Console.Clear();
                return namn;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }

    public string GetAddress()
    {
        while (true)
        {
            Console.WriteLine("Ange adress: ");
            var adress = Console.ReadLine();
            try
            {
                KundValidering.ValidateAdress(adress);
                Console.Clear();
                return adress;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }

    public string GetCity()
    {
        while (true)
        {
            Console.WriteLine("Ange stad: ");
            var city = Console.ReadLine();
            try
            {
                KundValidering.ValidateCity(city);
                Console.Clear();
                return city;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }

    public int GetZipCode()
    {
        while (true)
        {
            Console.WriteLine("Ange postnummer: ");
            var zipCodeInput = Console.ReadLine();
            try
            {
                if (int.TryParse(zipCodeInput, out int zipCode))
                {
                    KundValidering.ValidateZipCode(zipCode);
                    Console.Clear();
                    return zipCode;
                }
                else
                {
                    Console.WriteLine("Fel: Postnumret måste vara ett heltal.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }

    public int GetPhoneNumber()
    {
        while (true)
        {
            Console.WriteLine("Ange mobilnummer: ");
            var phoneNumberInput = Console.ReadLine();
            try
            {
                if (int.TryParse(phoneNumberInput, out int phoneNumber))
                {
                    KundValidering.ValidatePhoneNumber(phoneNumber);
                    Console.Clear();
                    return phoneNumber;
                }
                else
                {
                    Console.WriteLine("Fel: Mobilnumret måste vara ett heltal.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }

    public string GetEmail()
    {
        while (true)
        {
            Console.WriteLine("Ange e-postadress: ");
            var email = Console.ReadLine();
            try
            {
                KundValidering.ValidateEmail(email);
                Console.Clear();
                return email;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
            }
        }
    }
    
}
