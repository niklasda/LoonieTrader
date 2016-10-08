using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LoonieTrader.App.Constants;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
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
            ComplexOrderCommand = new RelayCommand(()=>OpenComplexOrderWindow(null));
            WorkbenchCommand = new RelayCommand(OpenWorkbenchWindow);
            NewChartCommand = new RelayCommand(()=>OpenNewChartWindow(null));
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
            ModifyPositionContextCommand = new RelayCommand(ModifyPosition);
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

            //AddToFavouritesCommand = new RelayCommand(AddInstrumentToFavourites);
            SelectedInstrumentChangedCommand = new RelayCommand<object>(SelectedInstrumentChanged);

            AddInstrumentToFavouritesContextCommand = new RelayCommand(AddInstrumentToFavourites);
            OpenInstrumentInMainContextCommand = new RelayCommand(OpenInstrumentInMain);
            OpenInstrumentInNewChartContextCommand = new RelayCommand(OpenInstrumentInNewChart);
            OpenInstrumentInTradeContextCommand = new RelayCommand(OpenInstrumentInTrade);

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
                    new CandleDataViewModel() {Date = "20160809", Time = "162100", High = 2m, Low = 0.3m, Open = 0.9m, Close = 1.7m},
                    new CandleDataViewModel() {Date = "20160810", Time = "162200", High = 2m, Low = 1m, Open = 1m, Close = 2m},
                    new CandleDataViewModel() {Date = "20160811", Time = "162300", High = 2.1m, Low = 1.1m, Open = 1.1m, Close = 2.1m}
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
                    AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetAccountInstruments(settings.DefaultAccountId);
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

                    its.Insert(0,new InstrumentTypeViewModel()
                    {
                        Type="Favourites",
                        Instruments=new InstrumentViewModel[]{ new InstrumentViewModel() { DisplayName="EUR/USD", Name="EUR_USD"}}
                    });
                    _allInstrumentTypes = its;

                    _accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);
                    _positionList = mapper.Map<IList<PositionViewModel>>(positionsResponse.positions);
                    //_orderList = mapper.Map<IList<OrderViewModel>>(ordersResponse.orders);
                    //_tradeList = mapper.Map<IList<TradeModel>>(tradesResponse.trades);
                    //_transactionList = mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;

                    MessageBox.Show("Failed to start application", AppProperties.ApplicationName);
                }
                // SetChartType("OHLC");

            }

            var dayConfig = Mappers.Financial<CandleDataViewModel>()
                .X(dayModel => (double) dayModel.DatePlusTime.Ticks / TimeSpan.FromHours(1).Ticks)
                .Open(dayModel => (double) dayModel.Open)
                .High(dayModel => (double) dayModel.High)
                .Low(dayModel => (double) dayModel.Low)
                .Close(dayModel => (double) dayModel.Close);
            SeriesCollection = new SeriesCollection(dayConfig);

            Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("s");

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

        public ICommand LogCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ExitApplicationCommand { get; set; }
        public ICommand ComplexOrderCommand { get; set; }
        public ICommand ReloadChartCommand { get; set; }
        public ICommand WorkbenchCommand { get; set; }
        public ICommand NewChartCommand { get; set; }
        public ICommand OpenPositionsCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand TransactionHistoryCommand { get; set; }
        public ICommand AccountInformationCommand { get; set; }
        public ICommand InstrumentInformationCommand { get; set; }

        public ICommand ClosePositionContextCommand { get; set; }
        public ICommand ModifyPositionContextCommand { get; set; }
        public ICommand CancelOrderContextCommand { get; set; }
        public ICommand ModifyOrderContextCommand { get; set; }

       // public ICommand AddToFavouritesCommand { get; set; }

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
            }
        }

        private void AddInstrumentToFavourites()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);
                AllInstrumentTypes[0].Instruments.a
            }
        }

        private void OpenInstrumentInMain()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);
                ReloadChart(SelectedInstrument);
            }
        }

        private void OpenInstrumentInNewChart()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);
                OpenNewChartWindow(SelectedInstrument);
            }
        }

        private void OpenInstrumentInTrade()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);
                OpenComplexOrderWindow(SelectedInstrument);
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

        public InstrumentViewModel SelectedInstrument
        {
            get { return _selectedInstrument; }
            set
            {
                if (_selectedInstrument != value)
                {
                    _selectedInstrument = value;
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
                string[] technicalIndicators = { "<No Indicator>", "Bollinger Band", "Accumulation Distribution", "Exponential Average",
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
                string[] technicalIndicators = { "<Chart Type>", "Candles", "OHLC" };
                return technicalIndicators;
            }
        }

        public string[] AvailableTimeframes
        {

            get
            {
                string[] technicalIndicators = { "<Select Time Frame>", "1 Min", "5 Min", "15 Min", "30 Min", "60 Min" };
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
        private void ModifyPosition()
        {
            Console.WriteLine(SelectedPosition.Instrument);

            OpenComplexOrderWindow(null);
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

            IList<CandleDataRecord> candleRecords = _dataLoader.LoadDataFile201603();

            var candleList = _mapper.Map<List<CandleDataViewModel>>(candleRecords);
            //var candleList2 = _mapper.Map<List<OhlcPoint>>(candleRecords);
            /*foreach (var candleDataViewModel in candleList)
            {
                GraphData.Add(candleDataViewModel);
            }
            */
            //ChartModel.CurrencyCode = instrument.DisplayName;
            //ChartModel.GraphData = new ObservableCollection<CandleDataViewModel>(candleList);

            if (SeriesCollection.Count < 1)
            {
                SeriesCollection.Add(
                    new OhlcSeries()
                    {
                        Title = string.Format("Reloaded {0}", instrument.DisplayName),
                        IncreaseBrush = Brushes.Green,
                        DecreaseBrush = Brushes.Red,
                        Values = new ChartValues<CandleDataViewModel>
                        {
                            new CandleDataViewModel() {Open=1.1m, High= 1.3m, Low=1.0m, Close=1.2m,Date = DateTime.Now.ToString("yyyyMMdd"), Time=DateTime.Now.ToString("HHmmss"), Ticker = instrument.DisplayName},
                        }
                    });

                //SeriesCollection.Add(
                //    new LineSeries
                //    {
                //        Title = "Line",
                //        Values = new ChartValues<double> {1.0, 1.0, 1.0, 1.0},
                //        Fill = Brushes.Transparent
                //    }
          //      );

                //Labels = new List<string>()
                //{
                //    DateTime.Now.ToString("dd MMM"),
                //    DateTime.Now.AddDays(1).ToString("dd MMM"),
                //    DateTime.Now.AddDays(2).ToString("dd MMM"),
                //    DateTime.Now.AddDays(3).ToString("dd MMM"),
                //    DateTime.Now.AddDays(4).ToString("dd MMM"),
                //};
            }
            else
            {
                SeriesCollection[0].Values.Add(new CandleDataViewModel() { Open = 1.1m, High = 1.3m, Low = 1.0m, Close = 1.2m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.ToString("HHmmss"), Ticker = instrument.DisplayName });
            //    Labels.Add("New");
            }

            // SetChartType(_chartType, instrument.DisplayName);
            // PlayTheData(candleList);
        }

        public Func<double, string> Formatter { get; set; }

        public RelayCommand<object> SelectedInstrumentChangedCommand { get; private set; }

        public ICommand AddInstrumentToFavouritesContextCommand { get; private set; }

        public ICommand OpenInstrumentInMainContextCommand { get; private set; }

        public ICommand OpenInstrumentInNewChartContextCommand { get; private set; }

        public ICommand OpenInstrumentInTradeContextCommand { get; private set; }

        private void SelectedInstrumentChanged(object o)
        {
            InstrumentViewModel instrument = o as InstrumentViewModel;
            InstrumentTypeViewModel instrumentType = o as InstrumentTypeViewModel;

            if (instrument != null)
            {
                SelectedInstrument = instrument;
                Console.WriteLine(instrument.DisplayName);
            }
            if (instrumentType != null)
            {
                SelectedInstrument = null;
                Console.WriteLine(instrumentType.Type);
            }
        }
       // public InstrumentViewModel SelectedCluster { get; private set; }


        private void OpenComplexOrderWindow(InstrumentViewModel instrument)
        {
            ComplexOrderWindow cow = new ComplexOrderWindow();
            cow.Owner = Application.Current.MainWindow;
            cow.ShowInstrument(instrument);
            //cow.Show();
        }

        private void OpenWorkbenchWindow()
        {
            WorkbenchWindow ww = new WorkbenchWindow();
            ww.Owner = Application.Current.MainWindow;
            ww.Show();
        }

        private void OpenNewChartWindow(InstrumentViewModel instrument)
        {
            ChartWindow tw = new ChartWindow();
            tw.Owner = Application.Current.MainWindow;
            tw.ShowInstrument(instrument);
            //tw.Show();

            //LiveChartWindow tdw = new LiveChartWindow();
            //tdw.Owner = Application.Current.MainWindow;
            //tdw.Show();
        }

        private void OpenSettingsWindow()
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Owner = Application.Current.MainWindow;
            sw.Show();
        }

        private void LogOut()
        {
            LoginWindow login = new LoginWindow();
            login.Owner = Application.Current.MainWindow;
            login.ShowDialog();
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