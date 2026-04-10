using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Application.Helpers;

public class ProduktOrderValidering
{
    public static void ValidateAntal(int antal)
    {
        if (antal <= 0)
            Console.WriteLine("Antal måste vara större än noll.");
    }

    public static void ValidateLager(int antal,int lagerAntal )
    {
        if (antal > lagerAntal)
            Console.WriteLine("Otillräckligt lagersaldo");
    }
}
