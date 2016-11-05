using System.Collections.Generic;
using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class BlotterWindowViewModel : ViewModelBase
    {
        public BlotterWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IAccountsRequester accountsRequester,
            IOrdersRequester ordersRequester, ITransactionsRequester transactionsRequester, IPositionsRequester positionsRequester)
        {
            _settings = settingsService.CachedSettings.SelectedEnvironment;

            _ordersRequester = ordersRequester;
            _transactionsRequester = transactionsRequester;
            _positionsRequester = positionsRequester;
            _mapper = mapper;


                    PositionsResponse positionsResponse = _positionsRequester.GetPositions(_settings.DefaultAccountId);
            _positionList = mapper.Map<IList<PositionViewModel>>(positionsResponse.positions);

                    AccountSummaryResponse accountSummaryResponse = accountsRequester.GetAccountSummary(_settings.DefaultAccountId);
            _accountSummary = mapper.Map<AccountSummaryViewModel>(accountSummaryResponse.account);


        }

        private readonly ITransactionsRequester _transactionsRequester;
        private readonly IPositionsRequester _positionsRequester;
        private readonly IOrdersRequester _ordersRequester;
        private readonly IEnvironmentSettings _settings;
        private readonly IMapper _mapper;

        private IList<PositionViewModel> _positionList;
        private IList<OrderViewModel> _orderList;
       // private IList<TradeViewModel> _tradeList;
        private IList<TransactionViewModel> _transactionList;
        private AccountSummaryViewModel _accountSummary;

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
                TransactionsResponse transactionsResponse = _transactionsRequester.GetTransactions(_settings.DefaultAccountId);
                _transactionList = _mapper.Map<IList<TransactionViewModel>>(transactionsResponse.transactions);

                return _transactionList;
            }
        }
    }
}