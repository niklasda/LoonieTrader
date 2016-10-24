using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IInstrumentRequester
    {
        CandlesResponse GetCandles(string instrument);

    }
}