using System.Net.Http.Json;
using Webshop.Domain.Interfaces;
using Webshop.Domain.Models;

namespace Webshop.Infrastructure.EF.Repositories;

public class ValutaRepository(HttpClient httpClient) : IValutaRepository
{
    private const string API_URL = "https://api.frankfurter.app/latest?from=SEK&to=USD,EUR,GBP";
    public async Task<Dictionary<string, decimal>> GetExchangeRateAsync()
    {
        var response = await httpClient.GetFromJsonAsync<FrankfurterResponse>(API_URL);
        return response?.Rates ?? new Dictionary<string, decimal>();
    }
}