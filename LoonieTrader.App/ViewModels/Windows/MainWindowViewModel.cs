using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;

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

               // var candleRecords = dataLoader.LoadDataFile201601();
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
                var candleRecords = dataLoader.LoadDataFile201601();
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
            get { return new[] {"asd", "wer"}; }
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