using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    public class TradeViewModel : ViewModelBase
    {
        public string Instrument { get; set; }

        public string Price { get; set; }
    }
}