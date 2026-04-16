using Webshop.Application.Helpers;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.ShopService;

public class ShopShoppingCart(Kundvagn kundvagn, ValutaSession valutaSession)
{
    public void ShowCartSelected()
    {
        for (int i = 0; i < kundvagn.Items.Count; i++)
        {
            var item = kundvagn.Items[i];
            Console.WriteLine($"{i + 1}. {item.Produkt.Namn} - {item.Antal} st - {valutaSession.FormatPris(item.PrisVidKöp)} st - {valutaSession.FormatPris(item.Delsumma)}");
        }
        Console.WriteLine($"Total summa: {valutaSession.FormatPris(kundvagn.TotalSumma)}");
    }

    public void ChangeStock()
    {
        var item = ChooseItem("Ange nummer på produkten du vill ändra antal på:");
        if (item == null) return;
        
        Console.WriteLine($"Ange nytt antal för {item.Produkt.Namn}:");
        if (!DataValidering.ValidateAntal(Console.ReadLine()!, out int nyttAntal))
        {
            Meny.Wait();
            return;
        }

        item.Antal = nyttAntal;
    }
    public void RemoveItem()
    {
        var item = ChooseItem("Ange nummer på produkten du vill ta bort:");
        if (item == null) return;
        kundvagn.RemoveItem(item.Produkt.Id);
    }

    private KundvagnItem? ChooseItem(string prompt)
    {
        Console.WriteLine(prompt);
        var input = Console.ReadLine();
        if (!DataValidering.ValidateListChoice(input!, kundvagn.Items.Count, out int val))
        {
            Meny.Wait();
            return null;
        }
        return kundvagn.Items[val - 1];
    }
}