using Webshop.Domain.Entitites;
using WebShop.Presentation.DisplayService.BetalVyService;

namespace WebShop.Presentation.Menu.Submenu;

public class BetalMenu(BetalVy betalVy)
{
    public async Task PaymentRun(Kund kund, FraktOmbud fraktOmbud)
    {
        Console.Clear();
        betalVy.SummaryPayment(fraktOmbud);

        var paymentMethod = betalVy.ChoosePaymentMethod();
        if (paymentMethod == null)
            return;

        Console.WriteLine($"Betalning sker med {paymentMethod}...");
        await Task.Delay(3000);

        await betalVy.SaveOrderAsync(kund, fraktOmbud, paymentMethod);
    }
}