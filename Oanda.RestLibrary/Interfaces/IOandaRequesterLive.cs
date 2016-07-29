using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IOandaRequesterLive
    {
        PricesResponse GetPrices(string instrument);
        PricesResponse GetCandles(string instrument);
        PricesResponse GetInstruments(string accountId);
    }
}