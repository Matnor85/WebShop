using WebShop.Presentation.DisplayService.CheckoutService;


namespace WebShop.Presentation.Menu.Submenu;

public class CheckOutMenu(CheckOut checkOut, BetalMenu betalMenu)
{
    public async Task FraktRun()
    {
        var kund = await checkOut.CreateCustomerAsync();
        var fraktOmbud = await checkOut.ChooseDelivery();
        if (fraktOmbud == null)
        {
            Console.WriteLine("Ingen fraktmetod vald.");
            return;
        }

        await betalMenu.PaymentRun(kund, fraktOmbud);
    }
}
