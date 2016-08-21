using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.RestLibrary.Caches;
using LoonieTrader.RestLibrary.HistoricalData;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class CompositeOrderWindowViewModel : ViewModelBase
    {
        public CompositeOrderWindowViewModel(IMapper mapper)
        {
            if (IsInDesignMode)
            {
                _allInstruments = new List<InstrumentViewModel>() { new InstrumentViewModel() { DisplayName = "EUR/USD" }, new InstrumentViewModel() { DisplayName = "USD/CAD" } };

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
                this._allInstruments = mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments);

               
            }
        }

        private IList<InstrumentViewModel> _allInstruments;
        public IList<InstrumentViewModel> AllInstruments
        {
            get
            {
                return _allInstruments;
            }
        }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public IList<string> AllAmounts
        {
            get { return new[] {"1 000", "10 000", "1000 000"}; }
        }
    }
}