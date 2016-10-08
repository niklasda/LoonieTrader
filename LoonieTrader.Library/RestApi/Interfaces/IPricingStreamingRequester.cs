using System;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IPricingStreamingRequester
    {
        IObservable<PricesResponse.Price> GetPriceStream(string accountId, string instrument);
    }
}