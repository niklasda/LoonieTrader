using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.ViewModels;
using LoonieTrader.Shared.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class WorkbenchWindowViewModel : ViewModelBase
    {
        public WorkbenchWindowViewModel(IMapper mapper, ISettingsService settingsService, IAccountsRequester accountsRequester, ChartBaseViewModel chartPart, CompositionContainer exporter)
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

            var cat = exporter.Catalog as DirectoryCatalog;
            cat?.Refresh();

            var laggers = exporter.GetExportedValues<ILaggingIndicator>();
            var leaders = exporter.GetExportedValues<ILeadingIndicator>();
            var algos = exporter.GetExportedValues<IAlgorithmicTrader>();

            FoundIndicators.AddRange(laggers);
            FoundIndicators.AddRange(leaders);
            FoundIndicators.AddRange(algos);
            // }

            if (IsInDesignMode)
            {
            }
            else
            {
                _allInstruments = mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments).OrderBy(x => x.Type).ThenBy(y => y.DisplayName).ToList();
            }
        }

        private IList<CandleDataViewModel> _sampleData;
        public IList<CandleDataViewModel> SampleData
        {
            get { return _sampleData; }
            set { _sampleData = value; }
        }

        public ChartBaseViewModel ChartPart { get; }

        private string _loadableInfo;
        public string LoadableInfo {
            get { return _loadableInfo;}
            set
            {
                if (_loadableInfo != value)
                {
                    _loadableInfo = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ILoadable _selectedIndicator;
        public ILoadable SelectedIndicator
        {
            get { return _selectedIndicator; }
            set
            {
                if (_selectedIndicator != value)
                {
                    _selectedIndicator = value;
                    DisplayInfo(_selectedIndicator);
                }
            }
        }

        private void DisplayInfo(ILoadable loadable)
        {
            LoadableInfo = loadable.Title;

            ChartPart.Instrument = new InstrumentViewModel() {DisplayName = "EUR_USD"};
            ChartPart.AddIndicator(v => (double)v.Open);
        }

        public List<ILoadable> FoundIndicators { get; } = new List<ILoadable>();

        private readonly IList<InstrumentViewModel> _allInstruments;

        public ObservableCollection<InstrumentViewModel> AllInstruments
        {
            get
            {
                List<InstrumentViewModel> filteredInstrumentList = _allInstruments.ToList();
                filteredInstrumentList.AddRange(_allInstruments.Except(filteredInstrumentList));
                return new ObservableCollection<InstrumentViewModel>(filteredInstrumentList);
            }
        }

        //private string _instrumentTest;
        //public string InstrumentText
        //{
        //    get { return _instrumentTest; }
        //    set
        //    {
        //        if (_instrumentTest != value)
        //        {
        //            _instrumentTest = value;
        //            RaisePropertyChanged();
        //            RaisePropertyChanged(() => AllInstruments);

        //        }
        //    }
        //}
    }
}