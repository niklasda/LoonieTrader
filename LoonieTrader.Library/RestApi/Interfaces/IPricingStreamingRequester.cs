using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces;

public interface IPricingStreamingRequester
{
    ObservableStream<PricesResponse.Price> GetPriceStream(string accountId, string instrument);
}