using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class KundValidering
{
    public static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Length != 11)
            Console.WriteLine("Inkorrekt format på mobilnumret");
    }
    public static void ValidateZipCode(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode) && zipCode.Length != 5)
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
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") && (!email.EndsWith(".se") && !email.EndsWith(".com")))
            Console.WriteLine("Inkorrekt format på e-postadressen");
    }
    public static async Task ValidateGuidExistAsync(Guid id, IKundService kundService)
    {
        if (!await kundService.ExistsAsync(id))
            throw new ArgumentException($"Kund med det angivna {id} finns inte");
    }
}
