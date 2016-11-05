using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ComplexOrderWindowViewModel : ViewModelBase
    {
        public ComplexOrderWindowViewModel(IMapper mapper, ISettingsService settingsService, IPricingRequester pricingRequester, IOrdersRequester orderRequester, IExtendedLogger logger, ChartPartViewModel chartPart)
        {
            _mapper = mapper;
            _settings = settingsService.CachedSettings.SelectedEnvironment;
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

            ChartPart = chartPart;

            if (IsInDesignMode)
            {
            }
            else
            {
                _allInstruments = _mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments).OrderBy(x=>x.Type).ThenBy(y=>y.DisplayName).ToList();
            }
        }

        public RelayCommand BuyCommand { get; set; }
        public RelayCommand SellCommand { get; set; }

        private readonly IMapper _mapper;
        private readonly IEnvironmentSettings _settings;
        private readonly IPricingRequester _pricingRequester;
        private readonly IOrdersRequester _orderRequester;
        private readonly IExtendedLogger _logger;

        private readonly IList<InstrumentViewModel> _allInstruments;

        public ChartPartViewModel ChartPart { get; private set; }

        public ObservableCollection<InstrumentViewModel> AllInstruments
        {
            get
            {
                List<InstrumentViewModel> filteredInstrumentList = _allInstruments.Where(x=>x.DisplayName.ToUpper().Contains((InstrumentText??"").ToUpper())|| x.Type.ToUpper().Contains((InstrumentText ?? "").ToUpper())).ToList();
                filteredInstrumentList.AddRange(_allInstruments.Except(filteredInstrumentList));
                return new ObservableCollection<InstrumentViewModel>(filteredInstrumentList);
            }
        }

        public ObservableCollection<PriceDepthViewModel> AllDepth
        {
            get
            {
                if (_latestPrice != null)
                {
                    IEnumerable<PriceDepthViewModel> bidDepth = _mapper.Map<IList<PriceDepthViewModel>>(_latestPrice.prices[0].bids);
                    IEnumerable<PriceDepthViewModel> askDepth = _mapper.Map<IList<PriceDepthViewModel>>(_latestPrice.prices[0].asks);

                    //IEnumerable<PriceDepthViewModel> depths = _latestPrice.prices[0].bids.Select(
                    //    x =>
                    //        new PriceDepthViewModel()
                    //        {
                    //            Bid = x.liquidity.ToString(),
                    //            Price = x.price,
                    //        });

                    //IEnumerable<PriceDepthViewModel> depths2 = _latestPrice.prices[0].asks.Select(
                    //    x =>
                    //        new PriceDepthViewModel()
                    //        {
                    //            Ask = x.liquidity.ToString(),
                    //            Price = x.price,
                    //        });

                    var depth = bidDepth.Concat(askDepth).ToArray();
                    return new ObservableCollection<PriceDepthViewModel>(depth);
                }

                return new ObservableCollection<PriceDepthViewModel>();
            }
        }

        private bool _isStopLossExpanded;
        public bool IsStopLossExpanded
        {
            get { return _isStopLossExpanded; }
            set
            {
                if (_isStopLossExpanded != value)
                {
                    _isStopLossExpanded = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => StopLossHeader);
                }
            }
        }

        private bool _isTakeProfitExpanded;
        public bool IsTakeProfitExpanded
        {
            get { return _isTakeProfitExpanded; }
            set
            {
                if (_isTakeProfitExpanded != value)
                {
                    _isTakeProfitExpanded = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(() => TakeProfitHeader);
                }
            }
        }

        public string TakeProfitHeader
        {
            get { return string.Format("Take-Profit {0}", IsTakeProfitExpanded ? "" : "[Not Set]"); }
        }

        public string StopLossHeader
        {
            get { return string.Format("Stop-Loss {0}", IsStopLossExpanded ? "" : "[Not Set]"); }
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
                    RaisePropertyChanged(() => IsNotGtcExpiry);
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
                    RaisePropertyChanged(() => IsNotMarketOrder);
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
                    RaisePropertyChanged(() => WindowTitle);

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
                ChartPart.Instrument = instrument;

                BuyButtonEnabled = false;
                BuyButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

                SellButtonEnabled = false;
                SellButtonLabel = string.Format("Loading {0}", instrument.DisplayName);

                decimal buyPrice = 0m;
                decimal sellPrice = 0m;

                // todo should be async
                LatestPrice = _pricingRequester.GetPrices(_settings.DefaultAccountId, instrument.Name);
                if (LatestPrice.prices.Length > 0 && LatestPrice.prices[0].asks.Length > 0)
                {
                    BuyButtonLabel = LatestPrice.prices[0].asks[0].price;
                    BuyButtonEnabled = true;

                    if (decimal.TryParse(LatestPrice.prices[0].asks[0].price, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentUICulture, out buyPrice))
                    {
                        MainPrice = buyPrice;
                        TakeProfitPrice = buyPrice + 0.2m;
                        StopLossPrice = buyPrice - 0.2m;
                    }
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

                    if (decimal.TryParse(LatestPrice.prices[0].bids[0].price, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentUICulture, out sellPrice))
                    {
                        MainPrice = sellPrice;
                        TakeProfitPrice = sellPrice + 0.2m;
                        StopLossPrice = sellPrice - 0.2m;
                    }
                }
                else
                {
                    SellButtonLabel = "No Bid Price";
                    SellButtonEnabled = false;
                }

                if (buyPrice != 0m && sellPrice != 0m)
                {
                  //  instrument.PipLocation
                   // var multiplier = Math.Pow(10, Math.Abs(instrument.PipLocation));
                    var multiplier = MathEx.IntPow(10, Math.Abs(instrument.PipLocation));
                    var spread =  Math.Abs(multiplier * buyPrice - multiplier * sellPrice).ToString("F", CultureInfo.CurrentUICulture);
                    BuySellSpread = string.Format("Spread: {0} pips", spread);
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

       // public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

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

        private DateTime[] _selectedExpiryDate = new DateTime[] { DateTime.Today.AddDays(4) };
        public DateTime[] SelectedExpiryDate
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

        private string _buySellSpread;
        public string BuySellSpread
        {
            get { return _buySellSpread; }
            set
            {
                if (_buySellSpread != value)
                {
                    _buySellSpread = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string WindowTitle
        {
            get { return string.Format("Complex Order: {0}", SelectedInstrument?.DisplayName); }
        }

        private string _instrumentTest;
        public string InstrumentText
        {
            get { return _instrumentTest; }
            set
            {
                if (_instrumentTest != value)
                {
                    _instrumentTest = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(()=>AllInstruments);

                }
            }
        }

        public InstrumentViewModel Instrument {
            get { return SelectedInstrument; }
            set
            {
                SelectedInstrument = value;
            }
        }
    }
}