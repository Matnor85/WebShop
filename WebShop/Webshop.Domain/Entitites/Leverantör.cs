namespace Webshop.Domain.Entitites;

public class Leverantör
{
    public Guid Id { get; set; }
    public string Namn { get; set; }
    public List<Produkt> Produkter { get; set; } = new List<Produkt>();

    public Leverantör() { }

    public Leverantör(string namn)
    {
        Id = Guid.NewGuid();
        Namn = namn;
    }
}