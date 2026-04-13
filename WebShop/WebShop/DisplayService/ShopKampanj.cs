using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;

namespace WebShop.Presentation.DisplayService;

public class ShopKampanj(IProduktKampanjService produktKampanjService, IProduktService produktService)
{
    public async Task ShowKampanjAsync()
    {
        var kampanjer = await produktKampanjService.GetAllAsync();
        if (!DataValidering.ValidateList(kampanjer, "Inga kampanjer hittades"))
            return;
        var toppKampanjer = kampanjer
            .OrderByDescending(k => k.Rabatt)
            .Take(3)
            .ToList();

        foreach (var kampanj in toppKampanjer)
        {
            Console.WriteLine($"Produkt: {kampanj.Produkt.Namn}");
            Console.WriteLine($"Ordinarie pris: {kampanj.Produkt.Pris:c}");
            Console.WriteLine($"Rabatt: {kampanj.Rabatt * 100}%");
            Console.WriteLine($"Kampanjpris: {kampanj.Produkt.Pris * (1 - kampanj.Rabatt):c}");
            Console.WriteLine();
        }
    }
}
