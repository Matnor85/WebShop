using Webshop.Application.Helpers;
using Webshop.Application.Interfaces;
using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.KundvagnService;
using WebShop.Presentation.DisplayService.ValutaApi;
using WebShop.Presentation.Menu;

namespace WebShop.Presentation.DisplayService.BetalVyService;

public class BetalVy(Kundvagn kundVagn, IOrderService orderService,IProduktService produktService, IProduktOrderService produktOrderService, ValutaSession valutaSession)
{
    public void SummaryPayment(FraktOmbud fraktOmbud)
    {
        Console.WriteLine("=== Betalning ===");
        PrintCartItems();
        PrintPaymentSummary(fraktOmbud);
    }

    private void PrintCartItems()
    {
        foreach (var item in kundVagn.Items)
        {
            Console.WriteLine($"{item.Produkt.Namn} - {item.Antal} st -  {valutaSession.FormatPris(item.Delsumma)}");
        }
    }

    private void PrintPaymentSummary(FraktOmbud fraktOmbud)
    {
        var moms = kundVagn.TotalSumma * 0.25m;
        Console.WriteLine($"Varor: {valutaSession.FormatPris(kundVagn.TotalSumma)}");
        Console.WriteLine($"Moms (25%): {valutaSession.FormatPris(moms)}");
        Console.WriteLine($"Frakt: {valutaSession.FormatPris(fraktOmbud.Pris)}");
        Console.WriteLine($"Totalt: {valutaSession.FormatPris(kundVagn.TotalSumma + moms + fraktOmbud.Pris)}");
    }

    public string? ChoosePaymentMethod()
    {
        Console.WriteLine("\n=== Välj betalningsmetod ===");

        PrintAvailablePaymentMethods();

        var input = Console.ReadLine()?.Trim().ToLower();
        if (!DataValidering.ValidateListChoice(input!, PaymentMethods.Count, out int choice))
        {
            Meny.Wait();
            return null;
        }
        return PaymentMethods[choice - 1];
    }

    public async Task SaveOrderAsync(Kund kund, FraktOmbud fraktOmbud, string betalningsMetod)
    {
        var moms = kundVagn.TotalSumma * 0.25m;

        var order = new Order
        {
           OrderDatum = DateTime.Now,
           TotalPris = kundVagn.TotalSumma + moms + fraktOmbud.Pris,
           KundId = kund.Id,
           FraktOmbudId = fraktOmbud.Id,
           BetalningsMetod = betalningsMetod
        };
        await orderService.AddAsync(order);

        foreach (var item in kundVagn.Items)
        {
            var produktOrder = new ProduktOrder
            {
                OrderId = order.Id,
                ProduktId = item.Produkt.Id,
                Antal = item.Antal,
                PrisvidKöp = item.PrisVidKöp
            };
            await produktOrderService.AddAsync(produktOrder);

            item.Produkt.LagerAntal -= item.Antal;
            await produktService.UpdateAsync(item.Produkt);
        }

        kundVagn.ClearCart();
        Console.WriteLine("Tack för din beställning!");
    }

    private static void PrintAvailablePaymentMethods()
    {
        for (int i = 0; i < PaymentMethods.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {PaymentMethods[i]}");
        }
    }

    private static readonly List<string> PaymentMethods = new()
    {
        "Kortbetalning",
        "PayPal",
        "Banköverföring"
    };
}