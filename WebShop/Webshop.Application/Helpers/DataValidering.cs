using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Application.Helpers;

public class DataValidering
{
    public static void ValidateName(string name)
    {
            if (string.IsNullOrWhiteSpace(name)) 
            Console.WriteLine("Inkorrekt format på namnet");
    }
    public static void ValidatePrice(decimal price)
    {
        if (price < 0) 
            Console.WriteLine("Priset måste vara större än eller lika med 0");
    }

    public static void ValidateId(Guid id)
    {
        if (id == Guid.Empty ) 
            Console.WriteLine("Id får inte vara tomt");
    }


}
