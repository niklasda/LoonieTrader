using LoonieTrader.Library.ViewModels;

namespace LoonieTrader.App.MessageTypes
{
    public class ChangeInstrumentMessage
    {
        public ChangeInstrumentMessage(InstrumentViewModel instrument)
        {
            Instrument = instrument;
        }

        public InstrumentViewModel Instrument { get;  }

    }
}