using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class PositionViewModel
    {
        [ReadOnly(true)]
        public string Instrument { get; set; }

        [ReadOnly(true)]
        public decimal ProfitLoss { get; set; }

        [ReadOnly(true), DisplayName("Resettable P/L")]
        public string ResettablePL { get; set; }

        [ReadOnly(true), DisplayName("Unrealized P/L")]
        public string UnrealizedPL { get; set; }
    }
}