using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class KundValidering
{
    //ändra Mobilnummer till string för svenska nummer kan börja med 070, 072, 073, 076, 079 och kan innehålla bindestreck
    public static void ValidatePhoneNumber(int phoneNumber)
    {
        if (phoneNumber < 100000000 || phoneNumber > 999999999)
            Console.WriteLine("Inkorrekt format på mobilnumret");
        
    }
    //ändra postnummer till string för svenska postnummer kan innehålla bokstäver
    public static void ValidateZipCode(int zipCode)
    {
        if (zipCode < 10000 || zipCode > 99999)
            Console.WriteLine("Inkorrekt format på postnumret");
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
