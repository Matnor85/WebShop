using Webshop.Domain.Entitites;

namespace WebShop.Presentation.DisplayService.KundvagnService;

public class KundvagnItem
{
    public Produkt Produkt { get; set; }
    public int Antal { get; set; }
    public decimal PrisVidKöp { get; set; }
    public decimal Delsumma => Antal * PrisVidKöp;
}
