using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.Models
{
    public class InstrumentType
    {
        public string Type { get; set; }

        public AccountInstrumentsResponse.Instrument[] Instruments { get; set; }
    }
}