using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Configuration;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.Shared;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettings settings, IMapper mapper, IHistoricalDataLoader dataLoader, IDialogService dialogService,
            IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester, ITransactionsRequester transactionsRequester)
        {
            _settings = settings;
            _mapper = mapper;
            _dataLoader = dataLoader;
            _dialogService = dialogService;

            _accountsRequester = accountsRequester;
            _ordersRequester = ordersRequester;
            _positionsRequester = positionsRequester;
            _tradesRequester = tradesRequester;
            _transactionsRequester = transactionsRequester;

            LogCommand = new RelayCommand(OpenLogWindow);
            AboutCommand = new RelayCommand(OpenAboutWindow);
            MarketOrderCommand = new RelayCommand(OpenMarketOrderWindow);
            CompositeOrderCommand = new RelayCommand(OpenCompositeOrderWindow);
            WorkbenchCommand = new RelayCommand(OpenWorkbenchWindow);
            NewChartCommand = new RelayCommand(OpenNewChartWindow);
            SettingsCommand = new RelayCommand(OpenSettingsWindow);
            LogOutCommand = new RelayCommand(LogOut);
            ReloadChartCommand = new RelayCommand(ReloadChart);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            OpenPositionsCommand = new RelayCommand(() => SelectedTabIndex = 0);
            OpenOrdersCommand = new RelayCommand(() => SelectedTabIndex = 1);
            TransactionHistoryCommand = new RelayCommand(() => SelectedTabIndex = 2);
            AccountInformationCommand = new RelayCommand(() => SelectedTabIndex = 3);
            InstrumentInformationCommand = new RelayCommand(() => SelectedTabIndex = 4);
            ClosePositionContextCommand = new RelayCommand(ClosePosition);
            CancelOrderContextCommand = new RelayCommand(CancelOrder);
            ModifyOrderContextCommand = new RelayCommand(ModifyOrder);

            ChartTypeCommand = new DelegateCommand<object>(ChartTypeChanged);
            IndicatorsChangedCommand = new DelegateCommand<object>(IndicatorsChanged);
            // TimeframesChangedCommand = new DelegateCommand<object>(ChartTypeChanged);
            // TradeTicketCommand = new DelegateCommand<object>(ChartTypeChanged);

            if (IsInDesignMode)
            {
                //_allInstruments = new List<InstrumentViewModel>() { new InstrumentViewModel() { DisplayName = "EUR/USD" }, new InstrumentViewModel() { DisplayName = "USD/CAD" } };
                _accountSummary = new AccountSummaryViewModel() { Id = "101" };
                _positionList = new List<PositionViewModel>() { new PositionViewModel() { Instrument = "EUR/USD" } };
                _orderList = new List<OrderViewModel>() { new OrderViewModel() { Instrument = "EUR/USD" } };
                _tradeList = new List<TradeModel>() { new TradeModel() { Instrument = "EUR/USD" } };
                _transactionList = new List<TransactionViewModel>() { new TransactionViewModel() { Instrument = "EUR/USD" } };

                // var candleRecords = dataLoader.LoadDataFile201603();
                // var candleList = mapper.Map<List<CandleDataViewModel>>(candleRecords);
                // GraphData = new ObservableCollection<CandleDataViewModel>(candleList);

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
                //  var candleRecords = _dataLoader.LoadDataFile201603();
                //  var candleList = _mapper.Map<List<CandleDataViewModel>>(candleRecords);
                // GraphData = new ConcurrentBag<CandleDataViewModel>();
                GraphData = new ObservableCollection<CandleDataViewModel>();

                // Task.Run(()=> PlayTheData(candleList));

                AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetInstruments(settings.DefaultAccountId);
                AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(settings.DefaultAccountId);
                OrdersResponse ordersResponse = _ordersRequester.GetOrders(settings.DefaultAccountId);
                PositionsResponse positionsResponse = _positionsRequester.GetPositions(settings.DefaultAccountId);
                TradesResponse tradesResponse = _tradesRequester.GetTrades(settings.DefaultAccountId);
                TransactionsResponse transactionsResponse = _transactionsRequester.GetTransactions(settings.DefaultAccountId);

                InstrumentCache.Instruments = instrumentsResponse.instruments;

                var allInstruments = mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments);

                // todo automapper
                var groups = allInstruments.Select(x => x).GroupBy(x => x.Type).OrderBy(o => o.Key);
                List<InstrumentTypeViewModel> its = groups.Select(x => new InstrumentTypeViewModel { Type = x.Key, Instruments = x.OrderBy(o => o.DisplayName).ToArray() }).ToList();

                _allInstrumentTypes = its;

                _accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);
                _positionList = mapper.Map<IList<PositionViewModel>>(positionsResponse.positions);
                _orderList = mapper.Map<IList<OrderViewModel>>(ordersResponse.orders);
                _tradeList = mapper.Map<IList<TradeModel>>(tradesResponse.trades);
                _transactionList = mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);
            }
        }

        private readonly ISettings _settings;
        private readonly IMapper _mapper;
        private IHistoricalDataLoader _dataLoader;
        private readonly IDialogService _dialogService;

        private readonly IAccountsRequester _accountsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly ITradesRequester _tradesRequester;
        private readonly ITransactionsRequester _transactionsRequester;

        private AccountSummaryViewModel _accountSummary;
        private IList<InstrumentTypeViewModel> _allInstrumentTypes;
        //private IList<InstrumentViewModel> _allInstruments;
        private IList<PositionViewModel> _positionList;
        private IList<OrderViewModel> _orderList;
        private IList<TradeModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;

        public SfChart MainChart { get; set; }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public RelayCommand LogCommand { get; set; }
        public RelayCommand AboutCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand LogOutCommand { get; set; }
        public RelayCommand ExitApplicationCommand { get; set; }
        public RelayCommand MarketOrderCommand { get; set; }
        public RelayCommand CompositeOrderCommand { get; set; }
        public RelayCommand ReloadChartCommand { get; set; }
        public RelayCommand WorkbenchCommand { get; set; }
        public RelayCommand NewChartCommand { get; set; }
        public RelayCommand OpenPositionsCommand { get; set; }
        public RelayCommand OpenOrdersCommand { get; set; }
        public RelayCommand TransactionHistoryCommand { get; set; }
        public RelayCommand AccountInformationCommand { get; set; }
        public RelayCommand InstrumentInformationCommand { get; set; }

        public RelayCommand ClosePositionContextCommand { get; set; }
        public RelayCommand CancelOrderContextCommand { get; set; }
        public RelayCommand ModifyOrderContextCommand { get; set; }

        public ICommand ChartTypeCommand { get; set; }
        public ICommand IndicatorsChangedCommand { get; set; }
        public ICommand TimeframesChangedCommand { get; set; }

        //public IList<InstrumentViewModel> AllInstruments
        // {
        //     get { return _allInstruments; }
        // }

        public IList<InstrumentTypeViewModel> AllInstrumentTypes
        {
            get { return _allInstrumentTypes; }
        }

        public IList<PositionViewModel> AllPositions
        {
            /*
             * *  AutoMapper
             * */
            get
            {
                // Positions + OpenPositions
                return _positionList;
            }
        }

        public IList<OrderViewModel> AllOrders
        {
            get
            {
                // return new[] // Orders + Pending Orders
                return _orderList;

            }
        }

        public IList<TransactionViewModel> AllTransactions
        {
            get
            {
                return _transactionList;
            }
        }

        public IList<TradeModel> AllTrades
        {
            get
            {
                return _tradeList;
            }
        }

        private void ChartTypeChanged(object checkedChartTypeItems)
        {
            var checkedChartTypes = checkedChartTypeItems as ObservableCollection<object>;
            if (checkedChartTypes != null)
            {
                this.MainChart.Series.Clear();

                foreach (string chartTypeName in checkedChartTypes)
                {
                    FinancialSeriesBase series = null;
                    if (chartTypeName == "Candles")
                    {
                        series = new CandleSeries();
                        series.Label = "Candles";
                    }
                    else if (chartTypeName == "OHLC")
                    {
                        series = new HiLoOpenCloseSeries();
                        series.Label = "OHLC";
                    }

                    if (series != null)
                    {
                        series.ItemsSource = GraphData;
                        series.Open = "Open";
                        series.High = "High";
                        series.Low = "Low";
                        series.Close = "Close";
                        series.XBindingPath = "Date";
                        series.ListenPropertyChange = true;

                        this.MainChart.Series.Add(series);
                    }
                }
            }
        }

        private void IndicatorsChanged(object checkedIndicatorItems)
        {
            var checkedIndicators = checkedIndicatorItems as ObservableCollection<object>;

            if (checkedIndicators != null)
            {
                this.MainChart.TechnicalIndicators.Clear();

                // FinancialTechnicalIndicator indicator = new AccumulationDistributionIndicator();
                // this.MainChart.TechnicalIndicators.Add(indicator);

                foreach (string indicatorName in checkedIndicators)
                {
                    var indicator = ApplyIndicator(indicatorName, 1);

                    // ISupportAxes2D indicatorAxis = indicator as ISupportAxes2D;
                    if (indicator != null)
                    {
                        this.MainChart.TechnicalIndicators.Add(indicator);
                        //   NumericalAxis axis = new NumericalAxis();
                        //   axis.OpposedPosition = true;
                        //   axis.ShowGridLines = false;
                        //   axis.Visibility = Visibility.Collapsed;
                        //   indicatorAxis.YAxis = axis;
                    }
                }
            }
        }

        private InstrumentViewModel _selectedInstrument;

        public object SelectedInstrument
        {
            get { return _selectedInstrument; }
            set
            {
                if (_selectedInstrument != value)
                {
                    _selectedInstrument = value as InstrumentViewModel;
                    RaisePropertyChanged();
                }
            }
        }

        public AccountSummaryViewModel AccountSummary
        {
            get { return _accountSummary; }
        }

        public string[] AvailableIndicators
        {
            get
            {
                string[] technicalIndicators = { "Bollinger Band", "Accumulation Distribution", "Exponential Average",
                                             "MACD", "Average True Range", "Momentum", "RSI", "Simple Average", "Stochastic",
                                             "Triangular Average"};
                return technicalIndicators;
            }
        }
        public ObservableCollection<object> SelectedIndicators
        {
            get
            {
                // Required to make first Command call work
                return new ObservableCollection<object>();
            }
        }

        public string[] AvailableChartTypes
        {

            get
            {
                string[] technicalIndicators = { "Candles", "OHLC" };
                return technicalIndicators;
            }
        }

        public string[] AvailableTimeframes
        {

            get
            {
                string[] technicalIndicators = { "1m", "5m", "15m", "30m", "60m" };
                return technicalIndicators;
            }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    RaisePropertyChanged();
                }
            }
        }

        private PositionViewModel _selectedPosition;
        public PositionViewModel SelectedPosition
        {
            get { return _selectedPosition; }
            set
            {
                if (_selectedPosition != value)
                {
                    _selectedPosition = value;
                    RaisePropertyChanged();
                }
            }
        }

        private OrderViewModel _selectedOrder;
        public OrderViewModel SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void OpenAboutWindow()
        {
            AboutWindow aw = new AboutWindow();
            aw.Owner = Application.Current.MainWindow;
            aw.ShowDialog();
        }

        private void OpenLogWindow()
        {
            LogWindow aw = new LogWindow();
            aw.Owner = Application.Current.MainWindow;
            aw.Show();
        }

        private void ClosePosition()
        {
            Console.WriteLine(SelectedPosition.Instrument);

            var message = string.Format("Close entire position in {0}", SelectedPosition.Instrument);

            if (_dialogService.AskYesNo(message))
            {
                _positionsRequester.PutClosePosition(_settings.DefaultAccountId, SelectedPosition.Instrument);
            }
        }

        private void CancelOrder()
        {
            Console.WriteLine(SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ModifyOrder()
        {
            Console.WriteLine(SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ReloadChart()
        {
            /*            while (GraphData.Count>0)
                        {
                            GraphData.RemoveAt(0);

                        }*/

            var candleRecords = _dataLoader.LoadDataFile201603();

            var candleList = _mapper.Map<List<CandleDataViewModel>>(candleRecords);
            /*foreach (var candleDataViewModel in candleList)
            {
                GraphData.Add(candleDataViewModel);
            }
            */
            GraphData = new ObservableCollection<CandleDataViewModel>(candleList);

            // PlayTheData(candleList);
        }

        private void OpenMarketOrderWindow()
        {
            MarketOrderWindow mow = new MarketOrderWindow();
            mow.Owner = Application.Current.MainWindow;
            mow.Show();
        }

        private void OpenCompositeOrderWindow()
        {
            CompositeOrderWindow cow = new CompositeOrderWindow();
            cow.Owner = Application.Current.MainWindow;
            cow.Show();
        }

        private void OpenWorkbenchWindow()
        {
            WorkbenchWindow ww = new WorkbenchWindow();
            ww.Show();
        }

        private void OpenNewChartWindow()
        {
            ChartWindow tw = new ChartWindow();
            tw.Show();
        }

        private void OpenSettingsWindow()
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }

        private void LogOut()
        {
        }

        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private FinancialTechnicalIndicator ApplyIndicator(string value, int rowIndex)
        {
            FinancialTechnicalIndicator indicator;
            switch (value)
            {
                case "Accumulation Distribution":
                    indicator = new AccumulationDistributionIndicator
                    {
                        Label = "Accumulation",
                        SignalLineColor = Brushes.Black
                    };
                    break;

                case "Average True Range":
                    indicator = new AverageTrueRangeIndicator
                    {
                        Label = "Average",
                        Period = 3,
                        SignalLineColor = Brushes.Black,
                    };
                    break;

                case "Bollinger Band":
                    indicator = new BollingerBandIndicator
                    {
                        Label = "Bollinger",
                        Period = 3,
                        UpperLineColor = Brushes.Blue,
                        LowerLineColor = Brushes.Red,
                        SignalLineColor = Brushes.Black,
                    };
                    break;
                case "Exponential Average":
                    indicator = new ExponentialAverageIndicator
                    {
                        Label = "Exponential",
                        Period = 3,
                        SignalLineColor = Brushes.Black
                    };
                    break;

                case "MACD": // Moving Average Convergence/Divergence
                    indicator = new MACDTechnicalIndicator
                    {
                        Label = "MACD",
                        Period = 2,
                        Type = MACDType.Line,
                        ShortPeriod = 3,
                        LongPeriod = 6,
                        SignalLineColor = Brushes.Black,
                        ConvergenceLineColor = Brushes.Green,
                        DivergenceLineColor = Brushes.Blue,
                    };
                    break;
                case "Momentum":
                    indicator = new MomentumTechnicalIndicator
                    {
                        Label = "Momentum",
                        Period = 4,
                        CenterLineColor = Brushes.Blue,
                        MomentumLineColor = Brushes.Black

                    };
                    break;
                case "RSI": // Relative Strength Index
                    indicator = new RSITechnicalIndicator
                    {
                        Label = "RSI",
                        Period = 4,
                        SignalLineColor = Brushes.Black,
                        UpperLineColor = Brushes.Blue,
                        LowerLineColor = Brushes.Red,
                    };
                    break;
                case "Simple Average":
                    indicator = new SimpleAverageIndicator
                    {
                        Label = "Simple",
                        Period = 3
                    };
                    break;
                case "Stochastic":
                    indicator = new StochasticTechnicalIndicator
                    {
                        Label = "Stochastic",
                        Period = 4,
                        KPeriod = 8,
                        DPeriod = 5,
                        UpperLineColor = Brushes.Blue,
                        LowerLineColor = Brushes.Red,
                        SignalLineColor = Brushes.Black,
                        PeriodLineColor = Brushes.Green
                    };
                    break;
                case "Triangular Average":
                    indicator = new TriangularAverageIndicator
                    {
                        Label = "Triangular",
                        Period = 4,
                        SignalLineColor = Brushes.Black
                    };
                    break;
                default:
                    return null;
            }

            var index = rowIndex == 0 ? 1 : 0;
            ChartSeries series = this.MainChart.VisibleSeries[index] as ChartSeries;
            indicator.XBindingPath = "Date";
            indicator.High = "High";
            indicator.Low = "Low";
            indicator.Open = "Open";
            indicator.Close = "Close";
            indicator.Volume = "Volume";

            Binding binding = new Binding();
            binding.Path = new PropertyPath("ItemsSource");
            binding.Source = series;
            binding.Mode = BindingMode.TwoWay;
            indicator.SetBinding(ChartSeriesBase.ItemsSourceProperty, binding);


            return indicator;
        }
    }

}