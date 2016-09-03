using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Caches
{
    public static class InstrumentCache
    {
        // persist the caches as yaml

        public static AccountInstrumentsResponse.Instrument[] Instruments { get; set; }
    }
}