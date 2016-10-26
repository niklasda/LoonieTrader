using System.Collections.Generic;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class WorkbenchWindowViewModel : ViewModelBase
    {
        public WorkbenchWindowViewModel(ISettingsService settings, IAccountsRequester accountsRequester, ChartPartViewModel chartPart)
        {
            ChartPart = chartPart;

            //if (IsInDesignMode)
            // {
            SampleData = new List<CandleDataViewModel>()
                {
                    new CandleDataViewModel() {Ticker = "EURUSD", Date = "20160808", Time = "162000", High = 2m, Low = 0.2m, Open = 0.6m, Close = 1.8m},
                    new CandleDataViewModel() {Ticker = "EURUSD", Date = "20160809", Time = "162000", High = 2m, Low = 0.3m, Open = 0.9m, Close = 1.7m},
                    new CandleDataViewModel() {Ticker = "EURUSD", Date = "20160810", Time = "162000", High = 2m, Low = 1m, Open = 1m, Close = 2m},
                    new CandleDataViewModel() {Ticker = "EURUSD", Date = "20160811", Time = "162000", High = 2.1m, Low = 1.1m, Open = 1.1m, Close = 2.1m}
                };
           // }
        }

        private IList<CandleDataViewModel> _sampleData;
        public IList<CandleDataViewModel> SampleData
        {
            get { return _sampleData; }
            set { _sampleData = value; }
        }

        public ChartPartViewModel ChartPart { get; private set; }

        public string[] FoundIndicators
        {
            get { return new string[] {"1", "2"}; }
        }
    }
}