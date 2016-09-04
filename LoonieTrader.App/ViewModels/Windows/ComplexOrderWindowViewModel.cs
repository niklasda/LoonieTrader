using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Caches;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Responses;
using Syncfusion.Windows.Shared;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class ComplexOrderWindowViewModel : ViewModelBase
    {
        public ComplexOrderWindowViewModel(IMapper mapper, ISettings settings, IPricingRequester pricingRequester, IOrdersRequester orderRequester, IExtendedLogger logger)
        {
            _mapper = mapper;
            _settings = settings;
            _pricingRequester = pricingRequester;
            _orderRequester = orderRequester;
            _logger = logger;

            MainPriceStep = 0.0001m; // todo fetch/calculate from instrument information
            MainPriceDecimals = 4;
            MainPrice = 1.4m;

            TakeProfitPriceStep = 0.0001m; // todo fetch/calculate from instrument information
            TakeProfitPriceDecimals = 4;
            TakeProfitPrice = 1.5m;

            StopLossPriceStep = 0.0001m; // todo fetch/calculate from instrument information
            StopLossPriceDecimals = 4;
            StopLossPrice = 1.3m;

            Amount = 100000m;
            AmountStep = 1000m;

            BuyCommand = new RelayCommand(CreateBuyOrder);
            SellCommand = new RelayCommand(CreateSellOrder);

            if (IsInDesignMode)
            {
                _allInstruments = new List<InstrumentViewModel>()
                {
                    new InstrumentViewModel() {DisplayName = "EUR/USD"},
                    new InstrumentViewModel() {DisplayName = "USD/CAD"}
                };

                GraphData = new ObservableCollection<CandleDataViewModel>()
                {
                    new CandleDataViewModel()
                    {
                        Date = "20160808", Time = "162000", High = 2m, Low = 0.2m, Open = 0.6m, Close = 1.8m
                    },
                    new CandleDataViewModel()
                    {
                        Date = "20160809", Time = "162000", High = 2m, Low = 0.3m, Open = 0.9m, Close = 1.7m
                    },
                    new CandleDataViewModel()
                    {
                        Date = "20160810",Time = "162000", High = 2m, Low = 1m, Open = 1m, Close = 2m
                    },
                    new CandleDataViewModel()
                    {
                        Date = "20160811", Time = "162000", High = 2.1m, Low = 1.1m, Open = 1.1m, Close = 2.1m
                    }
                };
            }
            else
            {
                this._allInstruments = _mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments);
            }
        }

        public RelayCommand BuyCommand { get; set; }
        public RelayCommand SellCommand { get; set; }
        public RelayCommand IsTrailingToggleCommand { get; set; }
        public RelayCommand IsGtcToggleCommand { get; set; }
        public RelayCommand IsMarketToggleCommand { get; set; }

        private readonly IMapper _mapper;
        private readonly ISettings _settings;
        private readonly IPricingRequester _pricingRequester;
        private readonly IOrdersRequester _orderRequester;
        private readonly IExtendedLogger _logger;

        private IList<InstrumentViewModel> _allInstruments;

        public IList<InstrumentViewModel> AllInstruments
        {
            get { return _allInstruments; }
        }

        public IList<PriceDepthViewModel> AllDepth
        {
            get
            {
                if (_latestPrice != null)
                {
                    // todo do this with automapper

                    IEnumerable<PriceDepthViewModel> depths = _latestPrice.prices[0].bids.Select(
                        x =>
                            new PriceDepthViewModel()
                            {
                                Bid = x.liquidity.ToString(),
                                Price = x.price,
                            });

                    IEnumerable<PriceDepthViewModel> depths2 = _latestPrice.prices[0].asks.Select(
                        x =>
                            new PriceDepthViewModel()
                            {
                                Ask = x.liquidity.ToString(),
                                Price = x.price,
                            });

                    var d2 = depths.Concat(depths2).ToArray();
                    return d2;
                }
                return new List<PriceDepthViewModel>(0);
            }
        }

        private bool _isTrailingStop;
        public bool IsTrailingStop
        {
            get { return _isTrailingStop; }
            set
            {
                if (_isTrailingStop != value)
                {
                    _isTrailingStop = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isGtcExpiry;
        public bool IsGtcExpiry
        {
            get { return _isGtcExpiry; }
            set
            {
                if (_isGtcExpiry != value)
                {
                    _isGtcExpiry = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(()=>IsNotGtcExpiry);
                }
            }
        }

        public bool IsNotGtcExpiry
        {
            get { return !IsGtcExpiry; }
        }


        private bool _isMarketOrder;
        public bool IsMarketOrder
        {
            get { return _isMarketOrder; }
            set
            {
                if (_isMarketOrder != value)
                {
                    _isMarketOrder = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(()=> IsNotMarketOrder);
                }
            }
        }

        public bool IsNotMarketOrder
        {
            get { return !IsMarketOrder; }
        }

        private InstrumentViewModel _selectedInstrument;
        public InstrumentViewModel SelectedInstrument
        {
            get { return _selectedInstrument; }
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

        private PricesResponse _latestPrice;
        public PricesResponse LatestPrice
        {
            get { return _latestPrice; }
            set
            {
                if (_latestPrice != value)
                {
                    _latestPrice = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => AllDepth);
                }
            }
        }

        private void CreateBuyOrder()
        {
            var od = new OrderCreateResponse.OrderDefinition();
            od.order = _mapper.Map<OrderCreateResponse.OrderDefinition.Order>(this);
            try
            {
                OrderCreateResponse ocr = _orderRequester.PostCreateOrder(_settings.DefaultAccountId, od);

            }
            catch (WebException wex)
            {
                _logger.Warning(wex, wex.Message);
                MessageBox.Show(Application.Current.MainWindow, wex.Message, "Buy Error");
            }
        }

        private void CreateSellOrder()
        {
            var od = new OrderCreateResponse.OrderDefinition();
            od.order = _mapper.Map<OrderCreateResponse.OrderDefinition.Order>(this);
            if (Amount > 0)
            {
                od.order.units = (-Amount).ToString();
            }

            try
            {
                OrderCreateResponse ocr = _orderRequester.PostCreateOrder(_settings.DefaultAccountId, od);

            }
            catch (WebException wex)
            {
                _logger.Warning(wex, wex.Message);
                MessageBox.Show(Application.Current.MainWindow, wex.Message, "Sell Error");
            }
        }


        private void LoadPrice(InstrumentViewModel instrument)
        {
            if (InstrumentExists(instrument))
            {
                BuyButtonEnabled = false;
                BuyButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

                SellButtonEnabled = false;
                SellButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

                // todo should be async
                LatestPrice = _pricingRequester.GetPrices(_settings.DefaultAccountId, instrument.Name);
                if (LatestPrice.prices.Length > 0 && LatestPrice.prices[0].asks.Length > 0)
                {
                    BuyButtonLabel = LatestPrice.prices[0].asks[0].price;
                    BuyButtonEnabled = true;
                }
                else
                {
                    BuyButtonLabel = "No Ask Price";
                    BuyButtonEnabled = false;
                }

                if (LatestPrice.prices.Length > 0 && LatestPrice.prices[0].bids.Length > 0)
                {
                    SellButtonLabel = LatestPrice.prices[0].bids[0].price;
                    SellButtonEnabled = true;
                }
                else
                {
                    SellButtonLabel = "No Bid Price";
                    SellButtonEnabled = false;
                }
            }
        }

        private bool InstrumentExists(InstrumentViewModel instrument)
        {
            if (!string.IsNullOrEmpty(instrument?.Name))
            {
                return InstrumentCache.Instruments.Any(x => x.name == instrument.Name);
            }

            return false;
        }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public IList<string> AllAmounts
        {
            get { return new[] { "1 000", "10 000", "1000 000" }; }
        }

        private string _buyButtonLabel = "BUY";
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

        private bool _buyButtonEnabled;
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

        private bool _sellButtonEnabled;
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

        private DatesCollection _selectedExpiryDate = new DatesCollection() { DateTime.Today.AddDays(4) };
        public DatesCollection SelectedExpiryDate
        {
            get
            {
                return _selectedExpiryDate;
            }
            set
            {
                _selectedExpiryDate = value;
            }
        }

        public string SelectedExpiryDateText
        {
            get
            {
                return _selectedExpiryDate[0].ToShortDateString();
            }
        }

        public DateTime EarliestExpiryDate
        {
            get { return DateTime.Today.AddDays(1); }
        }

        public DateTime LatestExpiryDate
        {
            get { return DateTime.Today.AddMonths(2); }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _amountStep;
        public decimal AmountStep
        {
            get { return _amountStep; }
            set
            {
                if (_amountStep != value)
                {
                    _amountStep = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _mainPrice;
        public decimal MainPrice
        {
            get { return _mainPrice; }
            set
            {
                if (_mainPrice != value)
                {
                    _mainPrice = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _mainPriceStep;
        public decimal MainPriceStep
        {
            get { return _mainPriceStep; }
            set
            {
                if (_mainPriceStep != value)
                {
                    _mainPriceStep = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _mainPriceDecimals;
        public int MainPriceDecimals
        {
            get { return _mainPriceDecimals; }
            set
            {
                if (_mainPriceDecimals != value)
                {
                    _mainPriceDecimals = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _takeProfitPrice;
        public decimal TakeProfitPrice
        {
            get { return _takeProfitPrice; }
            set
            {
                if (_takeProfitPrice != value)
                {
                    _takeProfitPrice = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _takeProfitPriceStep;
        public decimal TakeProfitPriceStep
        {
            get { return _takeProfitPriceStep; }
            set
            {
                if (_takeProfitPriceStep != value)
                {
                    _takeProfitPriceStep = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _takeProfitPriceDecimals;
        public int TakeProfitPriceDecimals
        {
            get { return _takeProfitPriceDecimals; }
            set
            {
                if (_takeProfitPriceDecimals != value)
                {
                    _takeProfitPriceDecimals = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _stopLossPrice;
        public decimal StopLossPrice
        {
            get { return _stopLossPrice; }
            set
            {
                if (_stopLossPrice != value)
                {
                    _stopLossPrice = value;
                    RaisePropertyChanged();
                }
            }
        }

        private decimal _stopLossPriceStep;
        public decimal StopLossPriceStep
        {
            get { return _stopLossPriceStep; }
            set
            {
                if (_stopLossPriceStep != value)
                {
                    _stopLossPriceStep = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _stopLossPriceDecimals;
        public int StopLossPriceDecimals
        {
            get { return _stopLossPriceDecimals; }
            set
            {
                if (_stopLossPriceDecimals != value)
                {
                    _stopLossPriceDecimals = value;
                    RaisePropertyChanged();
                }
            }
        }

       
    }
}