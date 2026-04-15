using Webshop.Application.Interfaces;

namespace Webshop.Application.Helpers;

public class FranktOmbudValendering
{
    public static bool IsValidFraktOmbud(string name, string address, string postalCode, string city)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(postalCode) || string.IsNullOrWhiteSpace(city))
        {
            return false;
        }

        if (postalCode.Length < 4 || postalCode.Length > 5)
        {
            return false;
        }
        return true;
    }
    public static async Task ValidateGuidExistAsync(Guid id, IFraktOmbudService fraktOmbudService)
    {
        if (!await fraktOmbudService.ExistsAsync(id))
            throw new ArgumentException($"FraktOmbud med det angivna {id} finns inte");
    }
}