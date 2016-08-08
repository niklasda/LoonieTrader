using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Windows;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettings settings, IMapper mapper, IAccountsRequester accountsRequester, IOrdersRequester ordersRequester, IPositionsRequester positionsRequester, ITradesRequester tradesRequester, ITransactionsRequester transactionsRequester)
        {
            _settings = settings;
            _mapper = mapper;
            _accountsRequester = accountsRequester;
            _ordersRequester = ordersRequester;
            _positionsRequester = positionsRequester;
            _tradesRequester = tradesRequester;
            _transactionsRequester = transactionsRequester;

            AboutCommand = new RelayCommand(About);
            TradeTicketCommand = new RelayCommand(TradeTicket);

            if (IsInDesignMode)
            {
                var plotModel = new PlotModel { Title = "Sample 1", Subtitle = "Graph" };
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 10 });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -1, Maximum = 10 });

                var lineSeries = new LineSeries { LineStyle = LineStyle.Solid };
                lineSeries.Points.AddRange(new[] { new DataPoint(1, 2), new DataPoint(2, 3) });

                plotModel.Series.Add(lineSeries);

                GraphData = plotModel;
            }
            else
            {
                var plotModel = new PlotModel { Title = "Example Live", Subtitle = "Graph" };
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 10 });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -1, Maximum = 10 });

                var lineSeries = new LineSeries { LineStyle = LineStyle.Solid };
                lineSeries.Points.AddRange(new[] { new DataPoint(1, 2), new DataPoint(2.1, 3) });

                plotModel.Series.Add(lineSeries);

                GraphData = plotModel;
            }

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

        private readonly ISettings _settings;
        private readonly IMapper _mapper;

        private readonly IAccountsRequester _accountsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly ITradesRequester _tradesRequester;
        private readonly ITransactionsRequester _transactionsRequester;

        private PlotModel _graphData;
        private AccountSummaryModel _accountSummary;
        private IList<InstrumentModel> _instrumentList;
        private IList<PositionModel> _positionList;
        private IList<OrderModel> _orderList;
        private IList<TradeModel> _tradeList;
        private IList<TransactionModel> _transactionList;

        public PlotModel GraphData
        {
            get { return _graphData; }
            set
            {
                if (_graphData != value)
                {
                    _graphData = value;
                    RaisePropertyChanged();
                }
            }
        }

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
    }
}