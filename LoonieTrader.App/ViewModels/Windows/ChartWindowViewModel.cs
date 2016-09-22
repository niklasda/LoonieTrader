using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using LoonieTrader.Library.HistoricalData;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class ChartWindowViewModel : ViewModelBase
    {
        public ChartWindowViewModel()
        {
            if (IsInDesignMode)
            {
                GraphData = new ObservableCollection<CandleDataViewModel>()
                {
                    new CandleDataViewModel() {Date = "20160808", Time = "162000", High = 2m, Low = 0.2m, Open = 0.6m, Close = 1.8m},
                    new CandleDataViewModel() {Date = "20160809", Time = "162000", High = 2m, Low = 0.3m, Open = 0.9m, Close = 1.7m},
                    new CandleDataViewModel() {Date = "20160810", Time = "162000", High = 2m, Low = 1m, Open = 1m, Close = 2m},
                    new CandleDataViewModel() {Date = "20160811", Time = "162000", High = 2.1m, Low = 1.1m, Open = 1.1m, Close = 2.1m}
                };
            }
            else
            {
                GraphData = new ObservableCollection<CandleDataViewModel>()
                {
                    new CandleDataViewModel() {Date = "20160808", Time = "162000", High = 2m, Low = 0.2m, Open = 0.6m, Close = 1.8m},
                    new CandleDataViewModel() {Date = "20160809", Time = "162000", High = 2m, Low = 0.3m, Open = 0.9m, Close = 1.7m},
                    new CandleDataViewModel() {Date = "20160810", Time = "162000", High = 2m, Low = 1m, Open = 1m, Close = 2m},
                    new CandleDataViewModel() {Date = "20160811", Time = "162000", High = 2.1m, Low = 1.1m, Open = 1.1m, Close = 2.1m}
                };
            }
        }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

    }
}