using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Enums;

namespace Webshop.Application.Helpers;

public class ProduktValidering
{
    public static bool ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Inkorrekt format på beskrivningen");
            return false;
        }
        return true;
    }
    
    public static void ValidateStock(int lagerAntal)
    {
        if (lagerAntal < 0) 
            Console.WriteLine("Lagerantal måste vara större än eller lika med 0");
    }

    public static async Task ValidateGuidExistAsync(Guid id, IProduktService produktService)
    {
        if (!await produktService.ExistAsync(id))
            Console.WriteLine($"Produkten med det angivna {id} finns inte");
    }
    
    public static void ValidateFärg(string färgInput, out Färg färg)
    {
        if (!Enum.TryParse(färgInput, out färg))
            Console.WriteLine("Ogiltigt färgformat");
    }

    public static void ValidateStorlek(string? storlekInput, out Storlek storlek)
    {
        if (!Enum.TryParse(storlekInput, out storlek))
            Console.WriteLine("Ogiltigt storlekformat");
    }
}
