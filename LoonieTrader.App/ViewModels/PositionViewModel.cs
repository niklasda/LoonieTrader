using System.ComponentModel;
using LoonieTrader.Library.RestApi.Caches;

namespace LoonieTrader.App.ViewModels
{
    public class PositionViewModel
    {
        public string DisplayName { get { return InstrumentCache.LookupDisplayName(Instrument); } }

        public string Instrument { get; set; }

        [DisplayName(@"Profit/Loss (P/L)")]
        public decimal ProfitLoss { get; set; }

        [DisplayName(@"Resettable P/L")]
        public string ResettablePL { get; set; }

        [DisplayName(@"Unrealized P/L")]
        public string UnrealizedPL { get; set; }
    }
}