
using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    public class PriceDepthViewModel : ViewModelBase
    {
        public string Bid { get; set; }

        public string Ask { get; set; }

        public string Price { get; set; }
    }
}