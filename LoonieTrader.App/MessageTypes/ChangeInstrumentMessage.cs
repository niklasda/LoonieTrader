using LoonieTrader.App.ViewModels;

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