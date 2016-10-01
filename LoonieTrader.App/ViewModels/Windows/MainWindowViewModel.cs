using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Configuration;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        enum GotoLocations
        {
            LocalAppData,
            Oanda,
            OandaApi,
            OandaDevForum,
            MarketPulse,
            MarketPulseCalendar,
            OandaNews,
            GoogleFinance,
            YahooFinance
        }

        public MainWindowViewModel(ISettings settings, IMapper mapper, IHistoricalDataLoader dataLoader, IDialogService dialogService,
            IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester,
            ITransactionsRequester transactionsRequester)
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
            ReloadChartCommand = new RelayCommand(() => ReloadChart(new InstrumentViewModel() {DisplayName = "EUR/USD"}));
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            OpenPositionsCommand = new RelayCommand(() => SelectedTabIndex = 0);
            OpenOrdersCommand = new RelayCommand(() => SelectedTabIndex = 1);
            TransactionHistoryCommand = new RelayCommand(() => SelectedTabIndex = 2);
            AccountInformationCommand = new RelayCommand(() => SelectedTabIndex = 3);
            InstrumentInformationCommand = new RelayCommand(() => SelectedTabIndex = 4);
            ClosePositionContextCommand = new RelayCommand(ClosePosition);
            CancelOrderContextCommand = new RelayCommand(CancelOrder);
            ModifyOrderContextCommand = new RelayCommand(ModifyOrder);

            ServiceStatusPracticeCommand = new RelayCommand(() => OpenServiceStatus(Environments.Practice));
            ServiceStatusLiveCommand = new RelayCommand(() => OpenServiceStatus(Environments.Live));

            GotoLocalSettingsFolderCommand = new RelayCommand(() => GotoLocation(GotoLocations.LocalAppData));
            GotoOandaCommand = new RelayCommand(() => GotoLocation(GotoLocations.Oanda));
            GotoOandaApiCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaApi));
            GotoOandaDevCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaDevForum));
            GotoMarketPulseCommand = new RelayCommand(() => GotoLocation(GotoLocations.MarketPulse));
            GotoMarketPulseCalendarCommand = new RelayCommand(() => GotoLocation(GotoLocations.MarketPulseCalendar));
            GotoNewsCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaNews));
            GotoGoogleFinanceCommand = new RelayCommand(() => GotoLocation(GotoLocations.GoogleFinance));
            GotoYahooFinanceCommand = new RelayCommand(() => GotoLocation(GotoLocations.YahooFinance));

          //  ChartTypeCommand = new DelegateCommand<object>(ChartTypeChanged);
          //  IndicatorsChangedCommand = new DelegateCommand<object>(IndicatorsChanged);
            // TimeframesChangedCommand = new DelegateCommand<object>(ChartTypeChanged);
            // TradeTicketCommand = new DelegateCommand<object>(ChartTypeChanged);

            if (IsInDesignMode)
            {
                //_allInstruments = new List<InstrumentViewModel>() { new InstrumentViewModel() { DisplayName = "EUR/USD" }, new InstrumentViewModel() { DisplayName = "USD/CAD" } };
                _accountSummary = new AccountSummaryViewModel() {Id = "101"};
                _positionList = new List<PositionViewModel>() {new PositionViewModel() {Instrument = "EUR/USD"}};
                _orderList = new List<OrderViewModel>() {new OrderViewModel() {Instrument = "EUR/USD"}};
                _tradeList = new List<TradeViewModel>() {new TradeViewModel() {Instrument = "EUR/USD"}};
                _transactionList = new List<TransactionViewModel>() {new TransactionViewModel() {Instrument = "EUR/USD"}};

                // var candleRecords = dataLoader.LoadDataFile201603();
                // var candleList = mapper.Map<List<CandleDataViewModel>>(candleRecords);
                // GraphData = new ObservableCollection<CandleDataViewModel>(candleList);

                ChartModel.GraphData = new ObservableCollection<CandleDataViewModel>()
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
                ChartModel.GraphData = new ObservableCollection<CandleDataViewModel>();

                // Task.Run(()=> PlayTheData(candleList));
                try
                {
                    AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetInstruments(settings.DefaultAccountId);
                    AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(settings.DefaultAccountId);
                    // OrdersResponse ordersResponse = _ordersRequester.GetOrders(settings.DefaultAccountId);
                    PositionsResponse positionsResponse = _positionsRequester.GetPositions(settings.DefaultAccountId);
                    // TradesResponse tradesResponse = _tradesRequester.GetTrades(settings.DefaultAccountId);
                    // TransactionsResponse transactionsResponse = _transactionsRequester.GetTransactions(settings.DefaultAccountId);

                    InstrumentCache.Instruments = instrumentsResponse.instruments;

                    var allInstruments = mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments);

                    // todo automapper
                    var groups = allInstruments.Select(x => x).GroupBy(x => x.Type).OrderBy(o => o.Key);
                    List<InstrumentTypeViewModel> its =
                        groups.Select(
                            x =>
                                new InstrumentTypeViewModel
                                {
                                    Type = x.Key,
                                    Instruments = x.OrderBy(o => o.DisplayName).ToArray()
                                }).ToList();

                    _allInstrumentTypes = its;

                    _accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);
                    _positionList = mapper.Map<IList<PositionViewModel>>(positionsResponse.positions);
                    //_orderList = mapper.Map<IList<OrderViewModel>>(ordersResponse.orders);
                    //_tradeList = mapper.Map<IList<TradeModel>>(tradesResponse.trades);
                    //_transactionList = mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);
                   ;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;

                    MessageBox.Show("Failed to start application", Constants.ApplicationName);
                }
                // SetChartType("OHLC");

            }
            SeriesCollection = new SeriesCollection();

            ReloadChart(new InstrumentViewModel() {DisplayName = "EURUSD"});
            
        }

        private void OpenServiceStatus(KeyValuePair<string, string> env)
        {
            Window window;
            if (env.Key == Environments.Live.Key)
            {
                window = new ServiceStatusLiveWindow();
            }
            else
            {
                window = new ServiceStatusPracticeWindow();
            }

            window.Owner = Application.Current.MainWindow;
            window.Show();
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
        private IList<TradeViewModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;

        //public SfChart MainChart { get; set; }

        //        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

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
        public ICommand ServiceStatusPracticeCommand { get; set; }
        public ICommand ServiceStatusLiveCommand { get; set; }

        public ICommand GotoLocalSettingsFolderCommand { get; set; }
        public ICommand GotoOandaCommand { get; set; }
        public ICommand GotoOandaApiCommand { get; set; }
        public ICommand GotoOandaDevCommand { get; set; }
        public ICommand GotoMarketPulseCommand { get; set; }
        public ICommand GotoMarketPulseCalendarCommand { get; set; }
        public ICommand GotoNewsCommand { get; set; }
        public ICommand GotoGoogleFinanceCommand { get; set; }
        public ICommand GotoYahooFinanceCommand { get; set; }

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

                // todo, maybe not reload everytime

                var ordersResponse = _ordersRequester.GetOrders(_settings.DefaultAccountId);
                _orderList = _mapper.Map<IList<OrderViewModel>>(ordersResponse.orders);

                return _orderList;

            }
        }

        public IList<TransactionViewModel> AllTransactions
        {
            get
            {
                // todo, maybe not reload everytime
                TransactionsResponse transactionsResponse = _transactionsRequester.GetTransactions(_settings.DefaultAccountId);
                _transactionList = _mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);

                return _transactionList;
            }
        }

        public IList<TradeViewModel> AllTrades
        {
            get
            {
                // todo not used
                return _tradeList;
            }
        }

        private List<string> _labels;


        public SeriesCollection SeriesCollection { get; set; }

        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                RaisePropertyChanged();
                //       OnPropertyChanged("Labels");
            }
        }

        private void GotoLocation(GotoLocations location)
        {
            switch (location)
            {
                case GotoLocations.LocalAppData:
                    SafeStart("%localappdata%\\LoonieTrader");
                    break;
                case GotoLocations.Oanda:
                    SafeStart("http://www.oanda.com");
                    break;
                case GotoLocations.OandaApi:
                    SafeStart("http://developer.oanda.com/rest-live-v20/introduction/");
                    break;
                case GotoLocations.OandaDevForum:
                    SafeStart("https://fxtrade.oanda.com/community/forex-forum/93/");
                    break;
                case GotoLocations.MarketPulse:
                    SafeStart("http://www.marketpulse.com/");
                    break;
                case GotoLocations.MarketPulseCalendar:
                    SafeStart("http://www.marketpulse.com/economic-events/");
                    break;
                case GotoLocations.OandaNews:
                    SafeStart("https://www.oanda.com/forex-trading/analysis/");
                    break;
                case GotoLocations.GoogleFinance:
                    SafeStart("https://www.google.com/finance?q=eurusd");
                    break;
                case GotoLocations.YahooFinance:
                    SafeStart("http://finance.yahoo.com/quote/EURUSD=X?p=EURUSD=X");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }

        }

        private void SafeStart(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                Process.Start(uri.ToString());

            }
            catch
            {
            }
        }


        public void ChangeChartInstrument(InstrumentViewModel instrument)
        {
            ReloadChart(instrument);
        }

        //private void ChartTypeChanged(object checkedChartTypeItems)
        //{
        //    var checkedChartTypes = checkedChartTypeItems as ObservableCollection<object>;
        //    if (checkedChartTypes != null)
        //    {

        //        //                foreach (string chartTypeName in checkedChartTypes)
        //        //              {
        //        string chartTypeName = checkedChartTypes.Cast<string>().FirstOrDefault();
        //        SetChartType(chartTypeName, "asd"); // todo fix
        //        // todo also move to chart view model

        //        //            }
        //    }
        //}

        //private string _chartType = "Candles";

        //private void SetChartType(string chartTypeName, string currencyCode)
        //{

        //    FinancialSeriesBase series = null;
        //    if (chartTypeName == "Candles")
        //    {
        //        _chartType = "Candles";

        //        series = new CandleSeries();
        //        series.Label = currencyCode;
        //    }
        //    else if (chartTypeName == "OHLC")
        //    {
        //        _chartType = "OHLC";

        //        series = new HiLoOpenCloseSeries();
        //        series.Label = currencyCode;
        //    }

        //    if (series != null && this.MainChart != null)
        //    {
        //        series.ItemsSource = ChartModel.GraphData;
        //        series.Open = "Open";
        //        series.High = "High";
        //        series.Low = "Low";
        //        series.Close = "Close";
        //        series.XBindingPath = "Date";
        //        series.ListenPropertyChange = true;

        //        this.MainChart.Series.Clear();
        //        this.MainChart.Series.Add(series);
        //    }
        //}

        //private void IndicatorsChanged(object checkedIndicatorItems)
        //{
        //    var checkedIndicators = checkedIndicatorItems as ObservableCollection<object>;

        //    if (checkedIndicators != null)
        //    {
        //        this.MainChart.TechnicalIndicators.Clear();

        //        // FinancialTechnicalIndicator indicator = new AccumulationDistributionIndicator();
        //        // this.MainChart.TechnicalIndicators.Add(indicator);

        //        foreach (string indicatorName in checkedIndicators)
        //        {
        //            var indicator = ApplyIndicator(indicatorName, 1);

        //            // ISupportAxes2D indicatorAxis = indicator as ISupportAxes2D;
        //            if (indicator != null)
        //            {
        //                this.MainChart.TechnicalIndicators.Add(indicator);
        //                //   NumericalAxis axis = new NumericalAxis();
        //                //   axis.OpposedPosition = true;
        //                //   axis.ShowGridLines = false;
        //                //   axis.Visibility = Visibility.Collapsed;
        //                //   indicatorAxis.YAxis = axis;
        //            }
        //        }
        //    }
        //}

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

        private string _selectedIndicator;
        public string SelectedIndicator
        {
            get
            {
                // Required to make first Command call work
                return _selectedIndicator;
            }
            set
            {
                _selectedIndicator = value; 
                ApplyIndicator(_selectedIndicator,0);
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

        public ChartViewModel ChartModel { get; } = new ChartViewModel();


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
            Console.WriteLine("Cancel: " + SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ModifyOrder()
        {
            Console.WriteLine("Modify: " + SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ReloadChart(InstrumentViewModel instrument)
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
            //ChartModel.CurrencyCode = instrument.DisplayName;
            //ChartModel.GraphData = new ObservableCollection<CandleDataViewModel>(candleList);


            SeriesCollection.Add(
                new OhlcSeries()
                {
                Title = "Reloaded",
                IncreaseBrush = Brushes.Green,
                DecreaseBrush = Brushes.Red,
                    Values = new ChartValues<OhlcPoint>
                    {
                        new OhlcPoint(32, 35, 30, 32),
                        new OhlcPoint(33, 38, 31, 37),
                        new OhlcPoint(35, 42, 30, 40),
                        new OhlcPoint(37, 40, 35, 38),
                        new OhlcPoint(35, 38, 32, 33)
                    }
                });
            
            SeriesCollection.Add(
                new LineSeries
                {
                    Title = "Line",
                    Values = new ChartValues<double> {30, 32, 35, 30},
                    Fill = Brushes.Transparent
                }
            );

            Labels = new List<string>()
            {
                DateTime.Now.ToString("dd MMM"),
                DateTime.Now.AddDays(1).ToString("dd MMM"),
                DateTime.Now.AddDays(2).ToString("dd MMM"),
                DateTime.Now.AddDays(3).ToString("dd MMM"),
                DateTime.Now.AddDays(4).ToString("dd MMM"),
            };
            // SetChartType(_chartType, instrument.DisplayName);
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

            LiveChartWindow tdw = new LiveChartWindow();
            tdw.Show();
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

        private void ApplyIndicator(string value, int rowIndex)
        {
            switch (value)
            {
                case "Accumulation Distribution":
                    break;

                case "Average True Range":
                    break;

                case "Bollinger Band":
                    break;
                case "Exponential Average":
                    break;

                case "MACD": // Moving Average Convergence/Divergence
                    break;
                case "Momentum":
                    break;
                case "RSI": // Relative Strength Index
                    break;
                case "Simple Average":
                    break;
                case "Stochastic":
                    break;
                case "Triangular Average":
                    break;
                default:
                    break;
            }

            SeriesCollection.Add(new CandleSeries()
            {
                Title = value,
                IncreaseBrush = Brushes.GreenYellow,
                DecreaseBrush = Brushes.OrangeRed,
                Values = new ChartValues<OhlcPoint>
                    {
                        new OhlcPoint(30, 5, 30, 32),
                        new OhlcPoint(30, 8, 31, 37),
                        new OhlcPoint(30, 2, 30, 40),
                        new OhlcPoint(30, 0, 35, 38)
                        }});

            //var index = rowIndex == 0 ? 1 : 0;
            //ChartSeries series = this.MainChart.VisibleSeries[index] as ChartSeries;
            //indicator.XBindingPath = "Date";
            //indicator.High = "High";
            //indicator.Low = "Low";
            //indicator.Open = "Open";
            //indicator.Close = "Close";
            //indicator.Volume = "Volume";

            //Binding binding = new Binding();
            //binding.Path = new PropertyPath("ItemsSource");
            //binding.Source = series;
            //binding.Mode = BindingMode.TwoWay;
            //indicator.SetBinding(ChartSeriesBase.ItemsSourceProperty, binding);


        }

        //private FinancialTechnicalIndicator ApplyIndicator(string value, int rowIndex)
        //{
        //    FinancialTechnicalIndicator indicator;
        //    switch (value)
        //    {
        //        case "Accumulation Distribution":
        //            indicator = new AccumulationDistributionIndicator
        //            {
        //                Label = "Accumulation",
        //                SignalLineColor = Brushes.Black
        //            };
        //            break;

        //        case "Average True Range":
        //            indicator = new AverageTrueRangeIndicator
        //            {
        //                Label = "Average",
        //                Period = 3,
        //                SignalLineColor = Brushes.Black,
        //            };
        //            break;

        //        case "Bollinger Band":
        //            indicator = new BollingerBandIndicator
        //            {
        //                Label = "Bollinger",
        //                Period = 3,
        //                UpperLineColor = Brushes.Blue,
        //                LowerLineColor = Brushes.Red,
        //                SignalLineColor = Brushes.Black,
        //            };
        //            break;
        //        case "Exponential Average":
        //            indicator = new ExponentialAverageIndicator
        //            {
        //                Label = "Exponential",
        //                Period = 3,
        //                SignalLineColor = Brushes.Black
        //            };
        //            break;

        //        case "MACD": // Moving Average Convergence/Divergence
        //            indicator = new MACDTechnicalIndicator
        //            {
        //                Label = "MACD",
        //                Period = 2,
        //                Type = MACDType.Line,
        //                ShortPeriod = 3,
        //                LongPeriod = 6,
        //                SignalLineColor = Brushes.Black,
        //                ConvergenceLineColor = Brushes.Green,
        //                DivergenceLineColor = Brushes.Blue,
        //            };
        //            break;
        //        case "Momentum":
        //            indicator = new MomentumTechnicalIndicator
        //            {
        //                Label = "Momentum",
        //                Period = 4,
        //                CenterLineColor = Brushes.Blue,
        //                MomentumLineColor = Brushes.Black

        //            };
        //            break;
        //        case "RSI": // Relative Strength Index
        //            indicator = new RSITechnicalIndicator
        //            {
        //                Label = "RSI",
        //                Period = 4,
        //                SignalLineColor = Brushes.Black,
        //                UpperLineColor = Brushes.Blue,
        //                LowerLineColor = Brushes.Red,
        //            };
        //            break;
        //        case "Simple Average":
        //            indicator = new SimpleAverageIndicator
        //            {
        //                Label = "Simple",
        //                Period = 3
        //            };
        //            break;
        //        case "Stochastic":
        //            indicator = new StochasticTechnicalIndicator
        //            {
        //                Label = "Stochastic",
        //                Period = 4,
        //                KPeriod = 8,
        //                DPeriod = 5,
        //                UpperLineColor = Brushes.Blue,
        //                LowerLineColor = Brushes.Red,
        //                SignalLineColor = Brushes.Black,
        //                PeriodLineColor = Brushes.Green
        //            };
        //            break;
        //        case "Triangular Average":
        //            indicator = new TriangularAverageIndicator
        //            {
        //                Label = "Triangular",
        //                Period = 4,
        //                SignalLineColor = Brushes.Black
        //            };
        //            break;
        //        default:
        //            return null;
        //    }

        //    var index = rowIndex == 0 ? 1 : 0;
        //    ChartSeries series = this.MainChart.VisibleSeries[index] as ChartSeries;
        //    indicator.XBindingPath = "Date";
        //    indicator.High = "High";
        //    indicator.Low = "Low";
        //    indicator.Open = "Open";
        //    indicator.Close = "Close";
        //    indicator.Volume = "Volume";

        //    Binding binding = new Binding();
        //    binding.Path = new PropertyPath("ItemsSource");
        //    binding.Source = series;
        //    binding.Mode = BindingMode.TwoWay;
        //    indicator.SetBinding(ChartSeriesBase.ItemsSourceProperty, binding);


        //    return indicator;
        //}
    }

}