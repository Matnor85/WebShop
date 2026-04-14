using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using WebShop.Presentation.DisplayService.ValutaApi;

namespace WebShop.Presentation.DisplayService.ShopService;

public class ShopKampanj(IProduktKampanjService produktKampanjService, IProduktService produktService, ValutaSession valutaSession)
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

            Console.WriteLine("=== Kampanjer ===");
        foreach (var kampanj in toppKampanjer)
        {
            Console.WriteLine($"Produkt: {kampanj.Produkt.Namn}");
            Console.WriteLine($"Ordinarie pris: {valutaSession.FormatPris(kampanj.Produkt.Pris)}");
            Console.WriteLine($"Rabatt: {kampanj.Rabatt * 100}%");
            Console.WriteLine($"Kampanjpris: {valutaSession.FormatPris(kampanj.Produkt.Pris * (1 - kampanj.Rabatt))}");
            Console.WriteLine("--------------------------");
        }
    }
}
