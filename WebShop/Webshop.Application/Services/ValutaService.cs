using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Webshop.Application.Interfaces;
using Webshop.Application.Models;

namespace Webshop.Application.Services;

public class ValutaService(HttpClient httpClient) : IValutaService
{
    private const string API_URL = "https://api.frankfurter.app/latest?from=SEK&to=USD,EUR,GBP";

    public async Task<decimal> ConvertCurrencyAsync(decimal pris, string tillValuta)
    {
        var rates = await GetExchangeRateAsync();
        if (!rates.TryGetValue(tillValuta, out decimal kurs))
            return pris;
        return pris * kurs;
    }

    public async Task<Dictionary<string, decimal>> GetExchangeRateAsync()
    {
        var response = await httpClient.GetFromJsonAsync<FrankfurterResponse>(API_URL);
        return response?.Rates ?? new Dictionary<string, decimal>();
    }

    
}
