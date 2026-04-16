namespace Webshop.Domain.Entitites;

public class ProduktKampanj
{
    public Guid Id { get; set; }
    public Guid ProduktId { get; set; }
    public Produkt Produkt { get; set; }
    public decimal Rabatt { get; set; }
    public ProduktKampanj() { }
}