using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.CheckoutService;

public class CheckOut(IFraktOmbudService fraktService, IKundService kundService, ValutaSession valutaSession)
{
    public async Task<Kund> CreateCustomerAsync()
    {
        Console.WriteLine("=== Kassa ===");
        Console.WriteLine("skapa konto för att beräkna fraktkostnad");
        var kund = KundInput();
        return await kundService.AddAsync(kund);
    }

    public async Task<FraktOmbud?> ChooseDelivery()
    {
        var fraktOmbudList = await fraktService.GetAllAsync();

        if (!DataValidering.ValidateList(fraktOmbudList, "Inga fraktombud tillgängliga"))
            return null;
        ShowFraktOmbudList(fraktOmbudList);
        var input = Console.ReadLine();
        if (!DataValidering.ValidateListChoice(input, fraktOmbudList.Count, out int val))
        {
            Meny.Wait();
            return null;
        }
        return fraktOmbudList[val - 1];
    }

    private void ShowFraktOmbudList(List<FraktOmbud> fraktOmbudList)
    {
        Console.WriteLine("=== Välj fraktmetod ===");
        for (int i = 0; i < fraktOmbudList.Count; i++)
        {
            Console.WriteLine($" {i + 1}. {fraktOmbudList[i].Namn} - {valutaSession.FormatPris(fraktOmbudList[i].Pris)}");
        }
    }

    private Kund KundInput()
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

            if (!DataValidering.ValidateName(namn))
                continue;

            Console.Clear();
            return namn;
        }
    }

    public string GetAddress()
    {
        while (true)
        {
            Console.WriteLine("Ange adress: ");
            var adress = Console.ReadLine();

            if (!KundValidering.ValidateAdress(adress))
                continue;

            Console.Clear();
            return adress;
        }
    }

    public string GetCity()
    {
        while (true)
        {
            Console.WriteLine("Ange stad: ");
            var city = Console.ReadLine();

            if (!KundValidering.ValidateCity(city))
                continue;

            Console.Clear();
            return city;
        }
    }

    public string GetZipCode()
    {
        while (true)
        {
            Console.WriteLine("Ange postnummer: ");
            var zipCode = Console.ReadLine();

            if (!KundValidering.ValidateZipCode(zipCode))
                continue;

            Console.Clear();
            return zipCode;
        }
    }

    public string GetPhoneNumber()
    {
        while (true)
        {
            Console.WriteLine("Ange mobilnummer: ");
            var phoneNumberInput = Console.ReadLine();


            if (!KundValidering.ValidatePhoneNumber(phoneNumberInput))
                continue;

            Console.Clear();
            return phoneNumberInput;

        }
    }

    public string GetEmail()
    {
        while (true)
        {
            Console.WriteLine("Ange e-postadress: ");
            var email = Console.ReadLine();

            if (!KundValidering.ValidateEmail(email))
                continue;

            Console.Clear();
            return email;

        }
    }
}
