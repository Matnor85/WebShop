using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Presentation.DisplayService.ValutaApi;

public class ValutaSession
{
    public string ValdValuta { get; set; } = "SEK";
    public decimal Kurs { get; set; } = 1m;
    public decimal KonverteraPris(decimal pris) => pris * Kurs;
}
