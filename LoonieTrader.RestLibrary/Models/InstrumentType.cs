using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.Models
{
    public class InstrumentType
    {
        public string Type { get; set; }

        public AccountInstrumentsResponse.Instrument[] Instruments { get; set; }
    }
}