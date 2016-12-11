using System.ComponentModel;

namespace LoonieTrader.Library.ViewModels
{
    [DisplayName(@"Instrument: ")]
    public class InstrumentViewModel
    {
        [DisplayName(@"Name ")]
        public string DisplayName { get; set; }

        [DisplayName(@"Precision ")]
        public int DisplayPrecision { get; set; }

        [DisplayName(@"Margin ")]
        public string MarginRate { get; set; }

        [DisplayName(@"Max Order ")]
        public string MaximumOrderUnits { get; set; }

        [DisplayName(@"Max Position ")]
        public string MaximumPositionSize { get; set; }

        [DisplayName(@"Max Trail Dist. ")]
        public string MaximumTrailingStopDistance { get; set; }

        [DisplayName(@"Max Trade Size ")]
        public string MinimumTradeSize { get; set; }

        [DisplayName(@"Min Trail Dist. ")]
        public string MinimumTrailingStopDistance { get; set; }

        [DisplayName(@"Id ")]
        public string Name { get; set; }

        public int PipLocation { get; set; }

        [DisplayName(@"Trade Prec. ")]
        public int TradeUnitsPrecision { get; set; }

        public string Type { get; set; }

        public override string ToString()
        {
            // Used by filter function
            return string.Format("{0}{1}{2}{3}", DisplayName, Name, Type, Name.Replace("_",""));
        }
    }
}