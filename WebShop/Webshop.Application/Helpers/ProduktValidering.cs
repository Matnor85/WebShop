using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Domain.Enums;

namespace Webshop.Application.Helpers;

public class ProduktValidering
{
    public static void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Inkorrekt format på beskrivningen");
    }
    
    public static void ValidateStock(int lagerAntal)
    {
        if (lagerAntal < 0) 
            throw new ArgumentException("Lagerantal måste vara större än eller lika med 0");
    }

    public static async Task ValidateGuidExistAsync(Guid id, IProduktService produktService)
    {
        if (!await produktService.ExistAsync(id))
            throw new ArgumentException($"Produkten med det angivna {id} finns inte");
    }
    
    public static void ValidateFärg(string färgInput, out Färg färg)
    {
        if (!Enum.TryParse(färgInput, out färg))
            throw new ArgumentException("Ogiltigt färgformat");
    }

    public static void ValidateStorlek(string? storlekInput, out Storlek storlek)
    {
        if (!Enum.TryParse(storlekInput, out storlek))
            throw new ArgumentException("Ogiltigt storlekformat");
    }
}
