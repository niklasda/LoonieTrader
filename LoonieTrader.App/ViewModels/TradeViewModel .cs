using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class TradeModel
    {
        [ReadOnly(true)]
        public string Instrument { get; set; }

        public string Price { get; set; }
    }
}