namespace Webshop.Application.Interfaces;

public interface IValutaService
{
    Task<decimal> ConvertCurrencyAsync(decimal pris, string tillValuta);
    Task<Dictionary<string, decimal>> GetExchangeRateAsync();
}