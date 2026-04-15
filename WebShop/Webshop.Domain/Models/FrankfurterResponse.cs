using System;
using System.Collections.Generic;
using System.Text;

namespace Webshop.Domain.Models;

public class FrankfurterResponse
{
    public Dictionary<string, decimal> Rates { get; set; } = new();
}
