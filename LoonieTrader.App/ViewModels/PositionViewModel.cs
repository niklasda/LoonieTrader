using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class PositionViewModel
    {
        [ReadOnly(true)]
        public string Instrument { get; set; }
        [ReadOnly(true)]
        public decimal ProfitLoss { get; set; }
        [ReadOnly(true)]
        public string ResettablePL { get; set; }
        [ReadOnly(true)]
        public string UnrealizedPL { get; set; }
    }
}