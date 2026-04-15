using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Application.Services;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.ValutaApi;

public class ValutaDisplay(IValutaService valutaService, ValutaSession valutaSession)
{
    public async Task ChooseRateAsync()
    {
        Console.Clear();
        var valuta = GetValutaVal();
        if (valuta == null) return;
        await ApplyExchangeRate(valuta);
        Meny.Wait();
    }

    private string? GetValutaVal()
    {
        Console.WriteLine("=== Välj valuta ===");
        Console.WriteLine("1. USD");
        Console.WriteLine("2. EUR");
        Console.WriteLine("3. GBP");
        Console.WriteLine("4. SEK");
        Console.WriteLine("5. Tillbaka");

        return Console.ReadLine() switch
        {
            "1" => "USD",
            "2" => "EUR",
            "3" => "GBP",
            "4" => "SEK",
            "5" => null
        };
    }

    private async Task ApplyExchangeRate(string valuta)
    {
        if (valuta == "SEK")
        {
            valutaSession.ValdValuta = "SEK";
            valutaSession.Kurs = 1m;
        }
        else
        {
            var kurser = await valutaService.GetExchangeRateAsync();
            if (kurser.TryGetValue(valuta, out var kurs))
            {
                valutaSession.ValdValuta = valuta;
                valutaSession.Kurs = kurs;
            }
        }
        Console.WriteLine($"Valuta satt till {valutaSession.ValdValuta}");
    }
}
