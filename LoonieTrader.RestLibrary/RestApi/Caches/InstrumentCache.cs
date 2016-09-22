using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Caches
{
    public static class InstrumentCache
    {
        // persist the caches as yaml

        public static AccountInstrumentsResponse.Instrument[] Instruments { get; set; }
    }
}