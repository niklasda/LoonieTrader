using Oanda.RestLibrary.Responses;

namespace Oanda.RestLibrary.Interfaces
{
    public interface IOandaRequesterLive
    {
        PricesResponse GetPrices(string instrument);
        PricesResponse GetCandles(string instrument);
        PricesResponse GetInstruments(string accountId);
    }
}