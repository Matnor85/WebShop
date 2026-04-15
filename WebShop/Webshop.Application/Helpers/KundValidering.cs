using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class KundValidering
{
    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10)
        {
            Console.WriteLine("Inkorrekt format på mobilnumret");
            return false;
        }
        return true;
    }
    public static bool ValidateZipCode(string zipCode)
    {
        if (string.IsNullOrWhiteSpace(zipCode) || zipCode.Length != 5)
        {
            Console.WriteLine("Inkorrekt format på postnumret");
            return false;
        }
        return true;
    }
    public static bool ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("Inkorrekt format på staden");
            return false;
        }
        return true;
    }
    public static bool ValidateAdress(string adress)
    {
        if (string.IsNullOrWhiteSpace(adress))
        {
            Console.WriteLine("Inkorrekt format på adressen");
            return false;
        }
        return true;
    }
    public static bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || (!email.EndsWith(".se") && !email.EndsWith(".com")))
        {
            Console.WriteLine("Inkorrekt format på e-postadressen");
            return false;
        }
        return true;
    }
    public static async Task ValidateGuidExistAsync(Guid id, IKundService kundService)
    {
        if (!await kundService.ExistsAsync(id))
            throw new ArgumentException($"Kund med det angivna {id} finns inte");
    }
}
