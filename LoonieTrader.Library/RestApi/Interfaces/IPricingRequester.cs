
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces;

public interface IPricingRequester
{
    PricesResponse GetPrices(string accountId, string instrument);

}