using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using JetBrains.Annotations;
using LoonieTrader.App.Constants;
using LoonieTrader.App.MessageTypes;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IHistoricalDataLoader dataLoader, IDialogService dialogService,
            //ChartPartViewModel chartPart, InstrumentsWindowViewModel instrumentsPart,
            IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester,
            ITransactionsRequester transactionsRequester)
        {
            _settingsService = settingsService;
            _settings = _settingsService.CachedSettings.SelectedEnvironment;
            _mapper = mapper;
            _logger = logger;
            _dataLoader = dataLoader;
            _dialogService = dialogService;

            //_accountsRequester = accountsRequester;
            //_ordersRequester = ordersRequester;
            //_positionsRequester = positionsRequester;
            //_tradesRequester = tradesRequester;
            //_transactionsRequester = transactionsRequester;

            //ChartPart = chartPart;
            //InstrumentsPart = instrumentsPart;

            LogCommand = new RelayCommand(OpenLogWindow);
            AboutCommand = new RelayCommand(OpenAboutWindow);
            ComplexOrderCommand = new RelayCommand(()=>OpenComplexOrderWindow(null));
            WorkbenchCommand = new RelayCommand(OpenWorkbenchWindow);
            BlotterCommand = new RelayCommand(OpenBlotterWindow);
            MachineLearningCommand = new RelayCommand(OpenMachineLearningWindow);
            InstrumentsCommand = new RelayCommand(OpenInstrumentsWindow);
            NewChartCommand = new RelayCommand(()=>OpenNewChartWindow(null));
            SettingsCommand = new RelayCommand(OpenSettingsWindow);
            LogOutCommand = new RelayCommand(LogOut);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            OpenPositionsCommand = new RelayCommand(() => BlotterPart.SelectedTabIndex = 0);
            OpenOrdersCommand = new RelayCommand(() => BlotterPart.SelectedTabIndex = 1);
            TransactionHistoryCommand = new RelayCommand(() => BlotterPart.SelectedTabIndex = 2);
            AccountInformationCommand = new RelayCommand(() => BlotterPart.SelectedTabIndex = 3);
            //InstrumentInformationCommand = new RelayCommand(() => BlotterPart.SelectedTabIndex = 4);

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

            var dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 5), DispatcherPriority.Normal, dispatcherTimer_Tick, Dispatcher.CurrentDispatcher);
            dispatcherTimer.Start();

            dispatcherTimer_Tick(null, EventArgs.Empty);

            Messenger.Default.Register<ChangeInstrumentMessage>(this, ChangeChartInstrument);

            if (IsInDesignMode)
            {

            }
            else
            {
                //try
               // {
                    //AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetAccountInstruments(_settings.DefaultAccountId);
                    //AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(_settings.DefaultAccountId);
                    //PositionsResponse positionsResponse = _positionsRequester.GetPositions(_settings.DefaultAccountId);

                    //InstrumentCache.Instruments = instrumentsResponse.instruments;

                    //var allInstruments = mapper.Map<IList<InstrumentViewModel>>(InstrumentCache.Instruments);

                    // todo automapper
                    //var groups = allInstruments.Select(x => x).GroupBy(x => x.Type).OrderBy(o => o.Key);
                    //List<InstrumentTypeViewModel> its =
                    //    groups.Select(x => new InstrumentTypeViewModel
                    //                        {
                    //                            Type = x.Key,
                    //                            Instruments = x.OrderBy(o => o.DisplayName).ToList()
                    //                        }).ToList();

                    //var favourites = new InstrumentTypeViewModel()
                    //{
                    //    Type = AppProperties.FavouritesFolderName,
                    //    Instruments = new List<InstrumentViewModel>()
                    //};

                    //its.Insert(0, favourites);

                    //foreach (var fi in _settings.FavouriteInstruments)
                    //{
                    //    var ivm = mapper.Map<InstrumentViewModel>(InstrumentCache.Lookup(fi));
                    //    if (ivm != null)
                    //    {
                    //        favourites.Instruments.Add(ivm);
                    //    }
                    //}

                    //_allInstrumentTypes = new ObservableCollection<InstrumentTypeViewModel>( its);

                    //_accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);

               // }
               // catch (Exception ex)
               // {
                  //  string msg = ex.Message;

                //    MessageBox.Show("Failed to start application", AppProperties.ApplicationName);
              //  }
            }

        }

        private readonly ISettingsService _settingsService;
        private readonly IEnvironmentSettings _settings;
        private readonly IMapper _mapper;
        private readonly IExtendedLogger _logger;
        private IHistoricalDataLoader _dataLoader;
        private readonly IDialogService _dialogService;

        //private readonly IAccountsRequester _accountsRequester;
        //private readonly IOrdersRequester _ordersRequester;
        //private readonly IPositionsRequester _positionsRequester;
        //private readonly ITradesRequester _tradesRequester;
        //private readonly ITransactionsRequester _transactionsRequester;

        //private AccountSummaryViewModel _accountSummary;
        //private readonly ObservableCollection<InstrumentTypeViewModel> _allInstrumentTypes;

        public ICommand LogCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ExitApplicationCommand { get; set; }
        public ICommand ComplexOrderCommand { get; set; }
        public ICommand WorkbenchCommand { get; set; }
        public ICommand InstrumentsCommand { get; set; }
        public ICommand BlotterCommand { get; set; }
        public ICommand MachineLearningCommand { get; set; }
        public ICommand NewChartCommand { get; set; }
        public ICommand OpenPositionsCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand TransactionHistoryCommand { get; set; }
        public ICommand AccountInformationCommand { get; set; }
        //public ICommand InstrumentInformationCommand { get; set; }

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

        public SciChartPartViewModel ChartPart
        {
            get
            {
                _chartPart = _chartPart ?? ServiceLocator.Current.GetInstance<SciChartPartViewModel>();
                return _chartPart;
            }
        }

        public InstrumentsWindowViewModel InstrumentsPart
        {
            get
            {
                _instrumentsPart = _instrumentsPart ?? ServiceLocator.Current.GetInstance<InstrumentsWindowViewModel>();
                return _instrumentsPart;
            }
        }

        public BlotterWindowViewModel BlotterPart
        {
            get
            {
                _blotterPart = _blotterPart ?? ServiceLocator.Current.GetInstance<BlotterWindowViewModel>();
                return _blotterPart;
            }
        }

        private SciChartPartViewModel _chartPart;
        private InstrumentsWindowViewModel _instrumentsPart;
        private BlotterWindowViewModel _blotterPart;

        private void GotoLocation(GotoLocations location)
        {
            switch (location)
            {
                case GotoLocations.LocalAppData:
                    // todo: some duplicated code
                    var appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var folder = Path.Combine(appDataFolderPath, AppProperties.ApplicationName);
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

        private void ChangeChartInstrument(ChangeInstrumentMessage instrument)
        {
            ChartPart.Instrument = instrument.Instrument;
        }

        public void ChangeChartInstrument(InstrumentViewModel instrument)
        {
            ChartPart.Instrument = instrument;
        }

        //private InstrumentViewModel _selectedInstrument;

        //public InstrumentViewModel SelectedInstrument
        //{
        //    get { return _selectedInstrument; }
        //    set
        //    {
        //        if (_selectedInstrument != value)
        //        {
        //            _selectedInstrument = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}

        //public AccountSummaryViewModel AccountSummary
        //{
        //    get { return _accountSummary; }
        //}

        public string[] AvailableIndicators
        {
            get
            {
                string[] technicalIndicators = { "<No Indicator>", "Bollinger Band", "Accumulation Distribution", "Exponential Average",
                                             "MACD", "Average True Range", "Momentum", "RSI", "Simple Average", "Stochastic",
                                             "Triangular Average"};

                SelectedIndicator = "<No Indicator>";

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

        private void OpenAboutWindow()
        {
            AboutWindow aw = new AboutWindow();
            //aw.Owner = Application.Current.MainWindow;
            aw.ShowDialog();
        }

        private void OpenLogWindow()
        {
            LogWindow aw = new LogWindow();
            //aw.Owner = Application.Current.MainWindow;
            aw.Show();
        }

        public string WindowTitle { get; } = AppProperties.ApplicationName;

        public string StatusBarLeft
        {
            get { return "Left part"; }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            StatusBarRight = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        private string _rightStatusText;
        public string StatusBarRight
        {
            get { return _rightStatusText; }
            set
            {
                if (_rightStatusText != value)
                {
                    _rightStatusText = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void OpenComplexOrderWindow(InstrumentViewModel instrument)
        {
            ComplexOrderWindow cow = new ComplexOrderWindow();
            //cow.Owner = Application.Current.MainWindow;
            cow.ShowInstrument(instrument);
        }

        private void OpenBlotterWindow()
        {
            BlotterWindow ww = new BlotterWindow();
            //ww.Owner = Application.Current.MainWindow;
            ww.Show();
        }

        private void OpenMachineLearningWindow()
        {
            var mlw = new MachineLearningWindow();
            //mlw.Owner = Application.Current.MainWindow;
            mlw.Show();
        }

        private void OpenInstrumentsWindow()
        {
            InstrumentsWindow ww = new InstrumentsWindow();
            //ww.Owner = Application.Current.MainWindow;
            ww.Show();
        }

        private void OpenWorkbenchWindow()
        {
            WorkbenchWindow ww = new WorkbenchWindow();
            //ww.Owner = Application.Current.MainWindow;
            ww.Show();
        }

        private void OpenNewChartWindow(InstrumentViewModel instrument)
        {
            ChartWindow tw = new ChartWindow();
            tw.Owner = Application.Current.MainWindow;
            tw.ShowInstrument(instrument);
            //tw.Show();
        }

        private void OpenServiceStatus()
        {
            Window window = new ServiceStatusWindow();

            window.Owner = Application.Current.MainWindow;
            window.Show();
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
            Messenger.Default.Unregister<ChangeInstrumentMessage>(this, ChangeChartInstrument);

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