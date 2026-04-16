namespace Webshop.Domain.Interfaces;

public interface IValutaRepository
{
    Task<Dictionary<string, decimal>> GetExchangeRateAsync();
}