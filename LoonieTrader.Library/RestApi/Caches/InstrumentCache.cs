using System.Linq;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Caches
{
    public static class InstrumentCache
    {
        // todo persist the caches as yaml

        public static AccountInstrumentsResponse.Instrument[] Instruments { get; set; }

        public static string LookupDisplayName(string instrumentName)
        {
            var displayName = Instruments.Where(w => w.name.Equals(instrumentName)).Select(i => i.displayName).FirstOrDefault();
            return displayName;
        }
    }
}