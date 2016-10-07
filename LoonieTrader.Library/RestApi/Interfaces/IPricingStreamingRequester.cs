using System;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IPricingStreamingRequester
    {
        IObservable<string> GetPriceStream(string accountId, string instrument);
    }
}