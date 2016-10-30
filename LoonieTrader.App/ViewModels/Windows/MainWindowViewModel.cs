using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.App.Constants;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Caches;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IHistoricalDataLoader dataLoader, IDialogService dialogService, ChartPartViewModel chartPart,
            IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester,
            ITransactionsRequester transactionsRequester)
        {
            _settingsService = settingsService;
            _settings = _settingsService.CachedSettings.SelectedEnvironment;
            _mapper = mapper;
            _logger = logger;
            _dataLoader = dataLoader;
            _dialogService = dialogService;

            _accountsRequester = accountsRequester;
            _ordersRequester = ordersRequester;
            _positionsRequester = positionsRequester;
            _tradesRequester = tradesRequester;
            _transactionsRequester = transactionsRequester;

            ChartPart = chartPart;

            LogCommand = new RelayCommand(OpenLogWindow);
            AboutCommand = new RelayCommand(OpenAboutWindow);
            ComplexOrderCommand = new RelayCommand(()=>OpenComplexOrderWindow(null));
            WorkbenchCommand = new RelayCommand(OpenWorkbenchWindow);
            NewChartCommand = new RelayCommand(()=>OpenNewChartWindow(null));
            SettingsCommand = new RelayCommand(OpenSettingsWindow);
            LogOutCommand = new RelayCommand(LogOut);
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

            ServiceStatusCommand = new RelayCommand(OpenServiceStatus);

            GotoLocalSettingsFolderCommand = new RelayCommand(() => GotoLocation(GotoLocations.LocalAppData));
            GotoProjectPageCommand = new RelayCommand(() => GotoLocation(GotoLocations.ProjectPage));
            GotoOandaCommand = new RelayCommand(() => GotoLocation(GotoLocations.Oanda));
            GotoOandaApiCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaApi));
            GotoOandaDevCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaDevForum));
            GotoMarketPulseCommand = new RelayCommand(() => GotoLocation(GotoLocations.MarketPulse));
            GotoMarketPulseCalendarCommand = new RelayCommand(() => GotoLocation(GotoLocations.MarketPulseCalendar));
            GotoNewsCommand = new RelayCommand(() => GotoLocation(GotoLocations.OandaNews));
            GotoGoogleFinanceCommand = new RelayCommand(() => GotoLocation(GotoLocations.GoogleFinance));
            GotoYahooFinanceCommand = new RelayCommand(() => GotoLocation(GotoLocations.YahooFinance));

            SelectedInstrumentChangedCommand = new RelayCommand<object>(SelectedInstrumentChanged);

            AddInstrumentToFavouritesContextCommand = new RelayCommand(AddInstrumentToFavourites);
            OpenInstrumentInMainContextCommand = new RelayCommand(OpenInstrumentInMain);
            OpenInstrumentInNewChartContextCommand = new RelayCommand(OpenInstrumentInNewChart);
            OpenInstrumentInTradeContextCommand = new RelayCommand(OpenInstrumentInTrade);

            if (IsInDesignMode)
            {
                _accountSummary = new AccountSummaryViewModel() {Id = "101"};
                _positionList = new List<PositionViewModel>() {new PositionViewModel() {Instrument = "EUR/USD"}};
                _orderList = new List<OrderViewModel>() {new OrderViewModel() {Instrument = "EUR/USD"}};
                _tradeList = new List<TradeViewModel>() {new TradeViewModel() {Instrument = "EUR/USD"}};
                _transactionList = new List<TransactionViewModel>() {new TransactionViewModel() {Instrument = "EUR/USD"}};

            }
            else
            {
                try
                {
                    AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetAccountInstruments(_settings.DefaultAccountId);
                    AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(_settings.DefaultAccountId);
                    PositionsResponse positionsResponse = _positionsRequester.GetPositions(_settings.DefaultAccountId);

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
                                    Instruments = x.OrderBy(o => o.DisplayName).ToList()
                                }).ToList();

                    its.Insert(0,new InstrumentTypeViewModel()
                    {
                        Type = AppProperties.FavouritesFolderName,
                        Instruments = new List<InstrumentViewModel>{ new InstrumentViewModel() { DisplayName="EUR/USD", Name="EUR_USD"}}
                    });

                    _allInstrumentTypes = new ObservableCollection<InstrumentTypeViewModel>( its);

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
            }

        }

        private void OpenServiceStatus()
        {
            Window window = new ServiceStatusWindow();

            window.Owner = Application.Current.MainWindow;
            window.Show();
        }


        private readonly ISettingsService _settingsService;
        private readonly IEnvironmentSettings _settings;
        private readonly IMapper _mapper;
        private readonly IExtendedLogger _logger;
        private IHistoricalDataLoader _dataLoader;
        private readonly IDialogService _dialogService;

        private readonly IAccountsRequester _accountsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly ITradesRequester _tradesRequester;
        private readonly ITransactionsRequester _transactionsRequester;

        private AccountSummaryViewModel _accountSummary;
        private readonly ObservableCollection<InstrumentTypeViewModel> _allInstrumentTypes;
        private IList<PositionViewModel> _positionList;
        private IList<OrderViewModel> _orderList;
        private IList<TradeViewModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;


        public ICommand LogCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ExitApplicationCommand { get; set; }
        public ICommand ComplexOrderCommand { get; set; }
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


        public ICommand ChartTypeCommand { get; set; }
        public ICommand IndicatorsChangedCommand { get; set; }
        public ICommand TimeframesChangedCommand { get; set; }
        public ICommand ServiceStatusCommand { get; set; }

        public ICommand GotoLocalSettingsFolderCommand { get; set; }
        public ICommand GotoProjectPageCommand { get; set; }
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

        public ObservableCollection<InstrumentTypeViewModel> AllInstrumentTypes
        {
            get { return _allInstrumentTypes; }
        }

        public ChartPartViewModel ChartPart { get; private set; }

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

        private void AddInstrumentToFavourites()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);

                var it = AllInstrumentTypes.FirstOrDefault(x => x.Type == AppProperties.FavouritesFolderName);
                if (it != null)
                {
                    bool exists = it.Instruments.Any(x => x.Name == SelectedInstrument.Name);
                    if (!exists)
                    {
                        it.Instruments.Add(SelectedInstrument);

                        it.RaisePropertyChanged(() => it.Instruments);

                        _settingsService.CachedSettings.SelectedEnvironment.FavouriteInstruments.Add(SelectedInstrument.Name);
                        _settingsService.SaveSettings(_settingsService.CachedSettings);
                    }
                }
            }
        }

        private void OpenInstrumentInMain()
        {
            if (SelectedInstrument != null)
            {
                Console.WriteLine(SelectedInstrument);
                ChangeChartInstrument(SelectedInstrument);
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
                    var folder = Path.Combine(Environment.ExpandEnvironmentVariables("%localappdata%"), AppProperties.ApplicationName);
                    SafeStartPath(folder);
                    break;
                case GotoLocations.ProjectPage:
                    SafeStartUri("http://niklasda.github.io");
                    break;
                case GotoLocations.Oanda:
                    SafeStartUri("http://www.oanda.com");
                    break;
                case GotoLocations.OandaApi:
                    SafeStartUri("http://developer.oanda.com/rest-live-v20/introduction/");
                    break;
                case GotoLocations.OandaDevForum:
                    SafeStartUri("https://fxtrade.oanda.com/community/forex-forum/93/");
                    break;
                case GotoLocations.MarketPulse:
                    SafeStartUri("http://www.marketpulse.com/");
                    break;
                case GotoLocations.MarketPulseCalendar:
                    SafeStartUri("http://www.marketpulse.com/economic-events/");
                    break;
                case GotoLocations.OandaNews:
                    SafeStartUri("https://www.oanda.com/forex-trading/analysis/");
                    break;
                case GotoLocations.GoogleFinance:
                    SafeStartUri("https://www.google.com/finance?q=eurusd");
                    break;
                case GotoLocations.YahooFinance:
                    SafeStartUri("http://finance.yahoo.com/quote/EURUSD=X?p=EURUSD=X");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }

        private void SafeStartUri([PathReference] string url)
        {
            try
            {
                Uri uri = new Uri(url);
                Process.Start(uri.ToString());
            }
            catch
            {
                // todo log
            }
        }

        private void SafeStartPath([PathReference] string path)
        {
            try
            {
                Process.Start("explorer.exe", path);
            }
            catch(Exception ex)
            {
                _logger.Warning(ex,"Failed to start: {0}", path);
            }
        }

        public void ChangeChartInstrument(InstrumentViewModel instrument)
        {
            ChartPart.Instrument = instrument;
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
                ApplyIndicator(_selectedIndicator);
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
            Console.WriteLine(@"Cancel: " + SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ModifyOrder()
        {
            Console.WriteLine(@"Modify: " + SelectedOrder.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        public RelayCommand<object> SelectedInstrumentChangedCommand { get; private set; }

        public ICommand AddInstrumentToFavouritesContextCommand { get; private set; }

        public ICommand OpenInstrumentInMainContextCommand { get; private set; }

        public ICommand OpenInstrumentInNewChartContextCommand { get; private set; }

        public ICommand OpenInstrumentInTradeContextCommand { get; private set; }

        public string WindowTitle { get; } = AppProperties.ApplicationName;

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


        private void OpenComplexOrderWindow(InstrumentViewModel instrument)
        {
            ComplexOrderWindow cow = new ComplexOrderWindow();
            cow.Owner = Application.Current.MainWindow;
            cow.ShowInstrument(instrument);
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

        [ContractAnnotation("=> halt")]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void ApplyIndicator(string value)
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
            }
        }
    }
}