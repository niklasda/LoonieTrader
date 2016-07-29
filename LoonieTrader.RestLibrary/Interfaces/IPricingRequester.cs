using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IPricingRequester
    {
        PricesResponse GetPrices(string instrument);

    }
}