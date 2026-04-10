using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class KundValidering
{
    //ändra Mobilnummer till string för svenska nummer kan börja med 070, 072, 073, 076, 079 och kan innehålla bindestreck
    public static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Inkorrekt format på mobilnumret");
    }
    //ändra postnummer till string för svenska postnummer kan innehålla bokstäver
    public static void ValidateZipCode(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
            throw new ArgumentException("Inkorrekt format på postnumret");
    }
    public static void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            Console.WriteLine("Inkorrekt format på staden");
    }
    public static void ValidateAdress(string adress)
    {
        if (string.IsNullOrWhiteSpace(adress))
            Console.WriteLine("Inkorrekt format på adressen");
    }
    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            Console.WriteLine("Inkorrekt format på e-postadressen");
    }
    public static async Task ValidateGuidExistAsync(Guid id, IKundService kundService)
    {
        if (!await kundService.ExistsAsync(id))
            throw new ArgumentException($"Kund med det angivna {id} finns inte");
    }
}
