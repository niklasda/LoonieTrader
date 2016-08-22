using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class WorkbgenchWindowViewModel : ViewModelBase
    {
        public WorkbgenchWindowViewModel(ISettings settings, IAccountsRequester accountsRequester)
        {
        }

    }
}