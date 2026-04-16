using System.Globalization;

namespace WebShop.Presentation.DisplayService.ValutaApi;

public class ValutaSession
{
    public string ValdValuta { get; set; } = "SEK";
    public decimal Kurs { get; set; } = 1m;
    public decimal KonverteraPris(decimal pris) => pris * Kurs;

    public string FormatPris(decimal pris)
    {
        var konverteratPris = KonverteraPris(pris);
        var culture = ValdValuta switch
        {
            "USD" => new CultureInfo("en-US"),
            "EUR" => new CultureInfo("de-DE"),
            "GBP" => new CultureInfo("en-GB"),
            _ => new CultureInfo("sv-SE")
        };
        return konverteratPris.ToString("C", culture);
    }
}