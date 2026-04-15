using Webshop.Application.Helpers;
using Webshop.Domain.Entitites;

namespace WebShop.Presentation.DisplayService.Helpers;

public class KundInputHelper
{
    public Kund KundInput()
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