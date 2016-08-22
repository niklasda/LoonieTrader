using System;
using System.ComponentModel;
using Syncfusion.Windows.Tools.Controls;

namespace LoonieTrader.App.ViewModels
{
    public class InstrumentViewModel
    {
        [ReadOnly(true)]
        public string DisplayName { get; set; }
        [ReadOnly(true)]
        public int DisplayPrecision { get; set; }
        [ReadOnly(true)]
        public string MarginRate { get; set; }
        [ReadOnly(true)]
        public string MaximumOrderUnits { get; set; }
        [ReadOnly(true)]
        public string MaximumPositionSize { get; set; }
        [ReadOnly(true)]
        public string MaximumTrailingStopDistance { get; set; }
        [ReadOnly(true)]
        public string MinimumTradeSize { get; set; }
        [ReadOnly(true)]
        public string MinimumTrailingStopDistance { get; set; }
        [ReadOnly(true)]
        public string Name { get; set; }
        [ReadOnly(true)]
        public int PipLocation { get; set; }
        [ReadOnly(true)]
        public int TradeUnitsPrecision { get; set; }
        [ReadOnly(true)]
        public string Type { get; set; }

    }
}