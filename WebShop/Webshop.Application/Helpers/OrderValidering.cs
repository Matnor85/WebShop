namespace Webshop.Application.Helpers;

public class OrderValidering
{
    public static void ValidateOrderDate(DateTime orderDatum)
    {
        if (orderDatum > DateTime.Now)
            Console.WriteLine("Orderdatum kan inte vara i framtiden.");
    }
}