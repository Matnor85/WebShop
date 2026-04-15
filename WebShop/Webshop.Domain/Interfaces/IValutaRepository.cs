using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Interfaces;

public interface IValutaRepository
{
    Task<Dictionary<string, decimal>> GetExchangeRateAsync();
}
