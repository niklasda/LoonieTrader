using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using LoonieTrader.Library.HistoricalData;

namespace LoonieTrader.App.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        private string _ccy;
        public string CurrencyCode {
            get { return _ccy; }
            set
            {
                if (_ccy != value)
                {
                    _ccy = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}