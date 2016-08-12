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

            AboutCommand = new RelayCommand(About);
            TradeTicketCommand = new RelayCommand(TradeTicket);

            if (IsInDesignMode)
            {
                _instrumentList = new List<InstrumentModel>();
                _accountSummary = new AccountSummaryModel();
                _positionList = new List<PositionModel>();
                _orderList = new List<OrderModel>();
                _tradeList = new List<TradeModel>();
                _transactionList = new List<TransactionModel>();

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

                _instrumentList = mapper.Map<IList<InstrumentModel>>(instrumentsResponse.instruments);
                _accountSummary = mapper.Map<AccountSummaryModel>(accountSummaryResponse.account);
                _positionList = mapper.Map<IList<PositionModel>>(positionsResponse.positions);
                _orderList = mapper.Map<IList<OrderModel>>(ordersResponse.orders);
                _tradeList = mapper.Map<IList<TradeModel>>(tradesResponse.trades);
                _transactionList = mapper.Map<IList<TransactionModel>>(transactionsResponse.transactions);


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

        private AccountSummaryModel _accountSummary;
        private IList<InstrumentModel> _instrumentList;
        private IList<PositionModel> _positionList;
        private IList<OrderModel> _orderList;
        private IList<TradeModel> _tradeList;
        private IList<TransactionModel> _transactionList;


        public SfChart MainChart { get; set; }

        public ObservableCollection<CandleDataViewModel> GraphData { get; set; }

        public RelayCommand AboutCommand { get; set; }

        public RelayCommand TradeTicketCommand { get; set; }

        public IList<InstrumentModel> InstrumentList
        {
            get
            {
                return _instrumentList;
            }
        }

        public IList<PositionModel> AllPositions
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

        public IList<OrderModel> AllOrders
        {
            get
            {
               // return new[] // Orders + Pending Orders
               return _orderList;

            }
        }

        public IList<TransactionModel> AllTransactions
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


        private ICommand selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (selectionChangedCommand == null)
                {
                    selectionChangedCommand = new DelegateCommand<object>(SelectionChagned);
                }
                return selectionChangedCommand;
            }
        }

        public void SelectionChagned(object checkedIndicatorItems)
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

        private InstrumentModel _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value as InstrumentModel; }
        }

        public AccountSummaryModel AccountSummary
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

        public void About()
        {
            AboutWindow aw = new AboutWindow();
            aw.Owner = Application.Current.MainWindow;
            aw.ShowDialog();

        }

        public void TradeTicket()
        {
            TradeTicketWindow tw = new TradeTicketWindow();
            tw.Show();
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