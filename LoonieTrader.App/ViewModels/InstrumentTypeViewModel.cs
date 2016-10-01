
using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    [DisplayName(@"Instrument Type")]
    public class InstrumentTypeViewModel
    {
        [ReadOnly(true)]
        public string Type { get; set; }

        [ReadOnly(true)]
        public InstrumentViewModel[] Instruments { get; set; }
    }
}