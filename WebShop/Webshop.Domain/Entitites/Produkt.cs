using System;
using System.Collections.Generic;
using System.Text;
using Webshop.Domain.Enums;

namespace Webshop.Domain.Entitites;

public class Produkt
{
    public Guid Id { get; set; }
    public string Namn { get; set; }
    public string Beskrivning { get; set; }
    public decimal Pris { get; set; }
    public Färg Färg { get; set; }
    public Storlek Storlek { get; set; }
    public int LagerAntal { get; set; }
    public Guid LeverantörId { get; set; }
    public Leverantör Leverantör { get; set; }
    public Guid KategoriId { get; set; }
    public Kategori Kategori { get; set; }
    public List<ProduktOrder> ProduktOrdrar { get; set; } = new();

    public Produkt() { }

    public Produkt(string namn, string beskrivning, decimal pris, Färg färg, Storlek storlek, int lagerAntal, Guid leverantörId, Guid kategoriId)
    {
        Id = Guid.NewGuid();
        Namn = namn;
        Beskrivning = beskrivning;
        Pris = pris;
        Färg = färg;
        Storlek = storlek;
        LagerAntal = lagerAntal;
        LeverantörId = leverantörId;
        KategoriId = kategoriId;
    }

    public override string ToString()
    {
        return $"Namn: {Namn}, Beskrivning: {Beskrivning}, Pris: {Pris}, Färg: {Färg}, Storlek: {Storlek}, LagerAntal: {LagerAntal}, Leverantörnamn: {Leverantör?.Namn}, Kategorinamn: {Kategori?.Namn}";
    }
}
