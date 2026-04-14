using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Application.Interfaces;

public interface IValutaService
{
    Task<decimal> ConvertCurrencyAsync(decimal pris, string tillValuta);
    Task<Dictionary<string, decimal>> GetExchangeRateAsync();
}
