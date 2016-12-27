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
    public class MachineLearningWindowViewModel : ViewModelBase
    {
        public MachineLearningWindowViewModel(ISettingsService settingsService, IMapper mapper, IExtendedLogger logger, IAccountsRequester accountsRequester, IDialogService dialogService,
            IOrdersRequester ordersRequester, ITransactionsRequester transactionsRequester, IPositionsRequester positionsRequester)
        {

        }

    }
}