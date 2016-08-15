using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using Syncfusion.UI.Xaml.Charts;
using Syncfusion.Windows.Shared;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettings settings, IMapper mapper, IHistoricalDataLoader dataLoader,
            IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester, ITransactionsRequester transactionsRequester)
        {

            _settings = settings;
            _mapper = mapper;
            _dataLoader = dataLoader;

            _accountsRequester = accountsRequester;
            _ordersRequester = ordersRequester;
            _positionsRequester = positionsRequester;
            _tradesRequester = tradesRequester;
            _transactionsRequester = transactionsRequester;

            AboutCommand = new RelayCommand(OpenAbout);
            TradeTicketCommand = new RelayCommand(OpenTradeTicket);
            NewChartCommand = new RelayCommand(OpenNewChart);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
            OpenPositionsCommand = new RelayCommand(() => SelectedTabIndex = 0);
            OpenOrdersCommand = new RelayCommand(() => SelectedTabIndex = 1);
            TransactionHistoryCommand = new RelayCommand(() => SelectedTabIndex = 2);
            AccountInformationCommand = new RelayCommand(() => SelectedTabIndex = 3);
            InstrumentInformationCommand = new RelayCommand(() => SelectedTabIndex = 4);

            // ChartTypeCommand = new DelegateCommand<object>(ChartTypeChanged);
            IndicatorsChangedCommand = new DelegateCommand<object>(IndicatorsChanged);
            // TimeframesChangedCommand = new DelegateCommand<object>(ChartTypeChanged);
            // TradeTicketCommand = new DelegateCommand<object>(ChartTypeChanged);

            if (IsInDesignMode)
            {
                _instrumentList = new List<InstrumentViewModel>() { new InstrumentViewModel() { DisplayName = "EUR/USD" } };
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
                var candleRecords = dataLoader.LoadDataFile201603();
                var candleList = mapper.Map<List<CandleDataViewModel>>(candleRecords);
                GraphData = new ObservableCollection<CandleDataViewModel>(candleList);

                AccountInstrumentsResponse instrumentsResponse = _accountsRequester.GetInstruments(settings.DefaultAccountId);
                AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(settings.DefaultAccountId);
                OrdersResponse ordersResponse = _ordersRequester.GetOrders(settings.DefaultAccountId);
                PositionsResponse positionsResponse = _positionsRequester.GetPositions(settings.DefaultAccountId);
                TradesResponse tradesResponse = _tradesRequester.GetTrades(settings.DefaultAccountId);
                TransactionsResponse transactionsResponse = _transactionsRequester.GetTransactions(settings.DefaultAccountId);

                _instrumentList = mapper.Map<IList<InstrumentViewModel>>(instrumentsResponse.instruments);
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

        private readonly IAccountsRequester _accountsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly ITradesRequester _tradesRequester;
        private readonly ITransactionsRequester _transactionsRequester;

        private AccountSummaryViewModel _accountSummary;
        private IList<InstrumentViewModel> _instrumentList;
        private IList<PositionViewModel> _positionList;
        private IList<OrderViewModel> _orderList;
        private IList<TradeModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;


        public SfChart MainChart { get; set; }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public RelayCommand AboutCommand { get; set; }
        public RelayCommand ExitApplicationCommand { get; set; }
        public RelayCommand TradeTicketCommand { get; set; }
        public RelayCommand NewChartCommand { get; set; }
        public RelayCommand OpenPositionsCommand { get; set; }
        public RelayCommand OpenOrdersCommand { get; set; }
        public RelayCommand TransactionHistoryCommand { get; set; }
        public RelayCommand AccountInformationCommand { get; set; }
        public RelayCommand InstrumentInformationCommand { get; set; }

        public ICommand ChartTypeCommand { get; set; }
        public ICommand IndicatorsChangedCommand { get; set; }
        public ICommand TimeframesChangedCommand { get; set; }

        public IList<InstrumentViewModel> InstrumentList
        {
            get
            {
                return _instrumentList;
            }
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


        private void IndicatorsChanged(object checkedIndicatorItems)
        {
            var checkedIndicators = checkedIndicatorItems as ObservableCollection<object>;
            this.MainChart.TechnicalIndicators.Clear();

            if (checkedIndicators != null)
            {

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
                string[] technicalIndicators = { "Bollinger Band", "Accumulation Distribution", "Exponential Average",
                                             "MACD", "Average True Range", "Momentum", "RSI", "Simple Average", "Stochastic",
                                             "Triangular Average"};
                return technicalIndicators;
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

        private void OpenAbout()
        {
            AboutWindow aw = new AboutWindow();
            aw.Owner = Application.Current.MainWindow;
            aw.ShowDialog();

        }

        private void OpenTradeTicket()
        {
            TradeTicketWindow tw = new TradeTicketWindow();
            tw.Show();
        }

        private void OpenNewChart()
        {
            ChartWindow tw = new ChartWindow();
            tw.Show();
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
                    indicator = new AccumulationDistributionIndicator();
                    break;

                case "Average True Range":
                    indicator = new AverageTrueRangeIndicator()
                    {
                        Period = 1
                    };
                    break;

                case "Bollinger Band":
                    indicator = new BollingerBandIndicator()
                    {
                        UpperLineColor = Brushes.Green,
                        Period = 3
                    };
                    break;
                case "Exponential Average":
                    indicator = new ExponentialAverageIndicator();
                    break;

                case "MACD":
                    indicator = new MACDTechnicalIndicator()
                    {
                        Period = 5,
                        LongPeriod = 12,
                        ShortPeriod = 6,
                        ConvergenceLineColor = Brushes.Green
                    };
                    break;
                case "Momentum":
                    indicator = new MomentumTechnicalIndicator()
                    {
                        Period = 4
                    };
                    break;
                case "RSI":
                    indicator = new RSITechnicalIndicator()
                    {
                        Period = 4,
                        UpperLineColor = Brushes.Green
                    };
                    break;
                case "Simple Average":
                    indicator = new SimpleAverageIndicator();
                    break;
                case "Stochastic":
                    indicator = new StochasticTechnicalIndicator()
                    {
                        UpperLineColor = Brushes.Green
                    };
                    break;
                case "Triangular Average":
                    indicator = new TriangularAverageIndicator();
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
            indicator.Close = "Last";
            indicator.Volume = "Volume";

            Binding binding = new Binding();
            binding.Path = new PropertyPath("ItemsSource");
            binding.Source = series;
            binding.Mode = BindingMode.TwoWay;
            indicator.SetBinding(FinancialTechnicalIndicator.ItemsSourceProperty, binding);


            return indicator;
        }
    }

}