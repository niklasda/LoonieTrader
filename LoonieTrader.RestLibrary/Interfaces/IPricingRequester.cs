using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IPricingRequester
    {
        PricesResponse GetPrices(string instrument);

    }
}