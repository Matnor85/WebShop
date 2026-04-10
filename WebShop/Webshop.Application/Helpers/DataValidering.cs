using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Application.Helpers;

public class DataValidering
{
    public static bool ValidateName(string name)
    {
            if (string.IsNullOrWhiteSpace(name)) 
            {
                Console.WriteLine("Inkorrekt format på namnet");
                return false;
            }
            return true;
    }
    public static bool ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            Console.WriteLine("Priset måste vara större än eller lika med 0");
            return false;
        }
        return true;
    }

    public static void ValidateId(Guid id)
    {
        if (id == Guid.Empty ) 
            Console.WriteLine("Id får inte vara tomt");
    }

    public static bool ValidateListChoice(string input, int count, out int val)
    {
        if (!int.TryParse(input, out val) || val < 1 || val > count)
        {
            Console.WriteLine("Ogiltigt val");
            return false;
        }
        return true;
    }

}
