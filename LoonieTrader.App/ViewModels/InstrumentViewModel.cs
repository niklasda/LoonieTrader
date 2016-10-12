using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    [DisplayName(@"Instrument: ")]
    public class InstrumentViewModel
    {
        public string DisplayName { get; set; }

        public int DisplayPrecision { get; set; }

        public string MarginRate { get; set; }

        public string MaximumOrderUnits { get; set; }

        public string MaximumPositionSize { get; set; }

        public string MaximumTrailingStopDistance { get; set; }

        public string MinimumTradeSize { get; set; }

        public string MinimumTrailingStopDistance { get; set; }

        public string Name { get; set; }

        public int PipLocation { get; set; }

        public int TradeUnitsPrecision { get; set; }

        public string Type { get; set; }

        public override string ToString()
        {
            // Used by filter function
            return string.Format("{0}{1}{2}{3}", DisplayName, Name, Type, Name.Replace("_",""));
        }
    }
}