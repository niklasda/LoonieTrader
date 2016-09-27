using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class TradeViewModel
    {
        [ReadOnly(true)]
        public string Instrument { get; set; }

        public string Price { get; set; }
    }
}