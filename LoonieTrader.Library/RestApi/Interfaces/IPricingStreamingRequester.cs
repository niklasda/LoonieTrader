using System.IO;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IPricingStreamingRequester
    {
        StreamReader GetPriceStream(string accountId, string instrument);
    }
}