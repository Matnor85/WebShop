namespace Webshop.Application.Helpers;

public class KampanjValidering
{
    public static bool ValidateRabatt(decimal rabatt)
    {
        if (rabatt < 0 || rabatt > 100)
        {
            Console.WriteLine("Rabatten måste vara mellan 0 och 100%.");
            return false;
        }
        return true;
    }
}