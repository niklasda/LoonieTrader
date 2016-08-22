using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using GalaSoft.MvvmLight;
using LoonieTrader.RestLibrary.Caches;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class CompositeOrderWindowViewModel : ViewModelBase
    {
        public CompositeOrderWindowViewModel(IMapper mapper, ISettings settings, IPricingRequester pricePricingRequester)
        {
            _settings = settings;
            _pricePricingRequester = pricePricingRequester;
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

        private readonly ISettings _settings;
        private readonly IPricingRequester _pricePricingRequester;

        private IList<InstrumentViewModel> _allInstruments;
        public IList<InstrumentViewModel> AllInstruments
        {
            get
            {
                return _allInstruments;
            }
        }

        private InstrumentViewModel _selectedInstrument;
        public InstrumentViewModel SelectedInstrument
        {
            get
            {
                return _selectedInstrument;
            }
            set
            {
                if (_selectedInstrument != value)
                {
                    _selectedInstrument = value;
                    RaisePropertyChanged();

                    LoadPrice(_selectedInstrument);
                }
            }
        }

        private void LoadPrice(InstrumentViewModel instrument)
        {
            BuyButtonEnabled = false;
            BuyButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

            SellButtonEnabled = false;
            SellButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

            // todo should be async
            var prices = _pricePricingRequester.GetPrices(_settings.DefaultAccountId, instrument.Name);
            BuyButtonLabel = prices.prices[0].asks[0].price;
            BuyButtonEnabled = true;
            SellButtonLabel = prices.prices[0].bids[0].price;
            SellButtonEnabled = true;

        }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public IList<string> AllAmounts
        {
            get { return new[] {"1 000", "10 000", "1000 000"}; }
        }

        private string _buyButtonLabel="BUY";
        public string BuyButtonLabel
        {
            get { return _buyButtonLabel; }
            set
            {
                if (_buyButtonLabel != value)
                {
                    _buyButtonLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _buyButtonEnabled=false;
        public bool BuyButtonEnabled
        {
            get { return _buyButtonEnabled; }
            set
            {
                if (_buyButtonEnabled != value)
                {
                    _buyButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _sellButtonLabel = "SELL";
        public string SellButtonLabel
        {
            get { return _sellButtonLabel; }
            set
            {
                if (_sellButtonLabel != value)
                {
                    _sellButtonLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _sellButtonEnabled = false;
        public bool SellButtonEnabled
        {
            get { return _sellButtonEnabled; }
            set
            {
                if (_sellButtonEnabled != value)
                {
                    _sellButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}