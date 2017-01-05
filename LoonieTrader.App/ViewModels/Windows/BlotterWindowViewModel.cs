using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class BlotterWindowViewModel : ViewModelBase
    {
        public BlotterWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IAccountsRequester accountsRequester, IDialogService dialogService,
            IOrdersRequester ordersRequester, ITransactionsRequester transactionsRequester, IPositionsRequester positionsRequester)
        {
            _settings = settingsService.CachedSettings.SelectedEnvironment;

            _ordersRequester = ordersRequester;
            _transactionsRequester = transactionsRequester;
            _positionsRequester = positionsRequester;
            _mapper = mapper;
            _dialogService = dialogService;


            PositionsResponse positionsResponse = _positionsRequester.GetPositions(_settings.DefaultAccountId);
            _positionList = mapper.Map<IList<PositionViewModel>>(positionsResponse.positions);

            AccountSummaryResponse accountSummaryResponse = accountsRequester.GetAccountSummary(_settings.DefaultAccountId);
            _accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);

            ClosePositionContextCommand = new RelayCommand(ClosePosition);
            ModifyPositionContextCommand = new RelayCommand(ModifyPosition);
            CancelOrderContextCommand = new RelayCommand(CancelOrder);
            ModifyOrderContextCommand = new RelayCommand(ModifyOrder);
        }

        private readonly ITransactionsRequester _transactionsRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IEnvironmentSettings _settings;
        private readonly IMapper _mapper;
        private readonly IDialogService _dialogService;

        private IList<PositionViewModel> _positionList;
        private IList<OrderViewModel> _orderList;
       // private IList<TradeViewModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;
        private AccountSummaryViewModel _accountSummary;
        public ICommand ClosePositionContextCommand { get; set; }
        public ICommand ModifyPositionContextCommand { get; set; }
        public ICommand CancelOrderContextCommand { get; set; }
        public ICommand ModifyOrderContextCommand { get; set; }

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

        public AccountSummaryViewModel AccountSummary
        {
            get { return _accountSummary; }
        }

        public IList<TransactionViewModel> AllTransactions
        {
            get
            {
                // todo, maybe not reload everytime
                TransactionsResponse transactionsResponse = _transactionsRequester.GetAllTransactions(_settings.DefaultAccountId);
                _transactionList = _mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);

                return _transactionList;
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
            Console.WriteLine(@"Cancel: " + SelectedOrder?.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void ModifyOrder()
        {
            Console.WriteLine(@"Modify: " + SelectedOrder?.Instrument);

            //MessageBox.Show(SelectedOrder.Instrument);
        }

        private void OpenComplexOrderWindow(InstrumentViewModel instrument)
        {
            ComplexOrderWindow cow = new ComplexOrderWindow();
            cow.Owner = Application.Current.MainWindow;
            cow.ShowInstrument(instrument);
        }
    }
}