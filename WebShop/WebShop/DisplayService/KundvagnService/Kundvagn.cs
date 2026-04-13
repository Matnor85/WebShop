using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Entitites;

namespace WebShop.Presentation.DisplayService.KundvagnService;

public class Kundvagn
{
    public List<KundvagnItem> Items { get; set; } = new();
    public decimal TotalSumma => Items.Sum(i => i.Delsumma);


    public void LäggTill(Produkt produkt, int antal)
    {
        var existingItem = Items.FirstOrDefault(i => i.Produkt.Id == produkt.Id);
        if (existingItem != null)
        {
            existingItem.Antal += antal;
        }
        else
        {
            Items.Add(new KundvagnItem { Produkt = produkt, Antal = antal, PrisVidKöp = produkt.Pris });
        }
    }

    public void TaBort(Guid produktId) => Items.RemoveAll(i => i.Produkt.Id == produktId);

    public void TömVagn() => Items.Clear();
}
