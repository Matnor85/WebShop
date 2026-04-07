using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace Webshop.Application.Helpers;

public class LeverantörValendering
{
    public static void IdValendering(Leverantör leverantör)
    {
        if (leverantör.Id == Guid.Empty)
        {
            leverantör.Id = Guid.NewGuid();
        }
    }

    public static void NamnValendering(Leverantör leverantör)
    {
        if (string.IsNullOrWhiteSpace(leverantör.Namn))
        {
            throw new ArgumentException("Namn får inte vara tomt.");
        }
    }
}
