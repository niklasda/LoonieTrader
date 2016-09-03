
using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Interfaces
{
    public interface IPricingRequester
    {
        PricesResponse GetPrices(string accountId, string instrument);

    }
}