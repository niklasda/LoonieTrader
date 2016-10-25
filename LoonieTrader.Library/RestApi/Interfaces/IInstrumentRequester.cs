using System;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IInstrumentRequester
    {
        CandlesResponse GetCandles(string instrument, CandlestickGranularity granularity = CandlestickGranularity.S10, string priceComponents = "M", int count = 4);

        CandlesResponse GetCandles(string instrument, DateTime startDate, CandlestickGranularity granularity = CandlestickGranularity.S10, string priceComponents = "M", int count = 4);

    }
}