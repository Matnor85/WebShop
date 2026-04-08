using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class OrderValidering
{
    public static void ValidateOrderDate(DateTime orderDatum)
    {
        if (orderDatum > DateTime.Now)
        {
            throw new ArgumentException("Orderdatum kan inte vara i framtiden.");
        }
    }
}
