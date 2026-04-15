using Webshop.Application.Interfaces;
using Webshop.Domain.Interfaces;

namespace Webshop.Application.Services;

public class ValutaService(IValutaRepository valutaRepository) : IValutaService
{
    public async Task<Dictionary<string, decimal>> GetExchangeRateAsync()
    {
        return await valutaRepository.GetExchangeRateAsync();
    }

    public async Task<decimal> ConvertCurrencyAsync(decimal pris, string tillValuta)
    {
        var rates = await GetExchangeRateAsync();
        if (!rates.TryGetValue(tillValuta, out decimal kurs))
            return pris;
        return pris * kurs;
    }
}