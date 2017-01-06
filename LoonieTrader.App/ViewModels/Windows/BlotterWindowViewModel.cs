using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class BlotterWindowViewModel : ViewModelBase
    {
        public BlotterWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IAccountsRequester accountsRequester, IDialogService dialogService,
            IOrdersRequester ordersRequester, ITransactionsRequester transactionsRequester, IPositionsRequester positionsRequester, ITransactionsStreamingRequester transactionsStreamingRequester)
        {
            _settings = settingsService.CachedSettings.SelectedEnvironment;

            _ordersRequester = ordersRequester;
            _transactionsRequester = transactionsRequester;
            _positionsRequester = positionsRequester;
            _transactionsStreamingRequester = transactionsStreamingRequester;
            _mapper = mapper;
            _accountsRequester = accountsRequester;
            _dialogService = dialogService;

            ReloadSomeLists();
            ObservableStream<TransactionsResponse.Transaction> ts = _transactionsStreamingRequester.GetTransactionStream(_settings.DefaultAccountId);
            ts.NewValue += Strm_NewTransaction;

            ClosePositionContextCommand = new RelayCommand(ClosePosition);
            ModifyPositionContextCommand = new RelayCommand(ModifyPosition);
            CancelOrderContextCommand = new RelayCommand(CancelOrder);
            ModifyOrderContextCommand = new RelayCommand(ModifyOrder);
        }

        private void Strm_NewTransaction(object sender, StreamEventArgs<TransactionsResponse.Transaction> e)
        {
            if (!e.Obj.type.Equals(AppProperties.HeartbeatName))
            {
                ReloadSomeLists();
            }

            Console.WriteLine(@"newTx: {0}", e.Obj.id);
        }

        private readonly ITransactionsRequester _transactionsRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly ITransactionsStreamingRequester _transactionsStreamingRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IEnvironmentSettings _settings;
        private readonly IMapper _mapper;
        private readonly IAccountsRequester _accountsRequester;
        private readonly IDialogService _dialogService;

        private ObservableCollection<PositionViewModel> _positionList;
        private ObservableCollection<OrderViewModel> _orderList;
       // private IList<TradeViewModel> _tradeList;
        private ObservableCollection<TransactionViewModel> _transactionList;
        private AccountSummaryViewModel _accountSummary;
        public ICommand ClosePositionContextCommand { get; set; }
        public ICommand ModifyPositionContextCommand { get; set; }
        public ICommand CancelOrderContextCommand { get; set; }
        public ICommand ModifyOrderContextCommand { get; set; }

        public ObservableCollection<PositionViewModel> AllPositions
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

        public ObservableCollection<OrderViewModel> AllOrders
        {
            get
            {
                // return new[] // Orders + Pending Orders

                // todo, maybe not reload everytime

                //var ordersResponse = _ordersRequester.GetOrders(_settings.DefaultAccountId);
                //_orderList = _mapper.Map<IList<OrderViewModel>>(ordersResponse.orders);

                return _orderList;
            }
        }

        public AccountSummaryViewModel AccountSummary
        {
            get { return _accountSummary; }
        }

        public ObservableCollection<TransactionViewModel> AllTransactions
        {
            get
            {
                // todo, maybe not reload everytime
               // TransactionsResponse transactionsResponse = _transactionsRequester.GetAllTransactions(_settings.DefaultAccountId);
               // _transactionList = _mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);

                return _transactionList;
            }
        }

        private void ReloadSomeLists(bool loadTransactions = false)
        {
             AccountSummaryResponse accountSummaryResponse = _accountsRequester.GetAccountSummary(_settings.DefaultAccountId);
            _accountSummary = _mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);
            RaisePropertyChanged(nameof(AccountSummary));

            PositionsResponse positionsResponse = _positionsRequester.GetPositions(_settings.DefaultAccountId);
            _positionList = new ObservableCollection<PositionViewModel>(_mapper.Map<IList<PositionViewModel>>(positionsResponse.positions));
            RaisePropertyChanged(nameof(AllPositions));

            var ordersResponse = _ordersRequester.GetOrders(_settings.DefaultAccountId);
            _orderList = new ObservableCollection<OrderViewModel>(_mapper.Map<IList<OrderViewModel>>(ordersResponse.orders));
            RaisePropertyChanged(nameof(AllOrders));

            if (loadTransactions)
            {
                TransactionsResponse transactionsResponse = _transactionsRequester.GetAllTransactions(_settings.DefaultAccountId);
                _transactionList = new ObservableCollection<TransactionViewModel>(_mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions).OrderByDescending(t => t.Id).ToList());
                RaisePropertyChanged(nameof(AllTransactions));
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

        private void ClosePosition()
        {
            if (SelectedPosition != null)
            {
                Console.WriteLine(SelectedPosition.Instrument);

                var message = string.Format("Close entire position in {0}", SelectedPosition.DisplayName);

                if (_dialogService.AskYesNo(message))
                {
                    _positionsRequester.PutClosePosition(_settings.DefaultAccountId, SelectedPosition.Instrument);
                   // ReloadLists();
                }
            }
        }

        private void ModifyPosition()
        {
            if (SelectedPosition != null)
            {
                Console.WriteLine(SelectedPosition.Instrument);

                var instrumentViewModel = new InstrumentViewModel() {Name = SelectedPosition.Instrument, DisplayName = SelectedPosition.DisplayName};
                OpenComplexOrderWindow(instrumentViewModel);
                //ReloadLists();
            }
        }

        private void CancelOrder()
        {
            if (SelectedOrder != null)
            {
                Console.WriteLine(@"Cancel: " + SelectedOrder?.Instrument);
                _ordersRequester.PutCancelOrder(_settings.DefaultAccountId, SelectedOrder.Id);
                //MessageBox.Show(SelectedOrder.Instrument);
             //   ReloadLists();
            }
        }

        private void ModifyOrder()
        {
            if (SelectedOrder != null)
            {
                Console.WriteLine(@"Modify: " + SelectedOrder?.Instrument);
                //ReloadLists();
                //MessageBox.Show(SelectedOrder.Instrument);
            }
        }

        private void OpenComplexOrderWindow(InstrumentViewModel instrument)
        {
            ComplexOrderWindow cow = new ComplexOrderWindow();
            cow.Owner = Application.Current.MainWindow;
            cow.ShowInstrument(instrument);
        }
    }
}