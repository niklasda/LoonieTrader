
namespace LoonieTrader.App.ViewModels
{
    public class CandleTimeViewModel
    {
        public string Ticker { get; set; }

        public string TimeFrame { get; set; }

        public CandleDataViewModel[] Candles { get; set; }
    }
}