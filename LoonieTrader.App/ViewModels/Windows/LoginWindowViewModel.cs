using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LoginWindowViewModel
    {
        public LoginWindowViewModel(ISettings settings, IAccountsRequester accountsRequester)
        {
            _accountsRequester = accountsRequester;
            LoginCommand = new RelayCommand(Login, () => CanClose);

            ApiKey = settings.ApiKey;
            SelectedEnvironmentKey = settings.Environment;

            _availableEnvironments =  new[] { Environments.Sandbox, Environments.Practice, Environments.Live };
        }

        private readonly IAccountsRequester _accountsRequester;
        private KeyValuePair<string, string>[] _availableEnvironments;

        public bool CanClose
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedEnvironmentKey)
                    && !string.IsNullOrWhiteSpace(ApiKey)
                    && ApiKey.Length == 65
                    && ApiKey.Split('-').Length == 2;
            }
        }

        public string ApiKey { get; set; }

        public string SelectedEnvironmentKey { get; set; }

        public string SelectedAccountKey { get; set; }

        public IList<KeyValuePair<string, string>> AvailableEnvironemnts
        {
            get { return _availableEnvironments; }
        }

        public RelayCommand LoginCommand { get; set; }

        public Action CloseAction { get; set; }

        public IList<KeyValuePair<string, string>> AvailableAccounts
        {
            get
            {
                var ar = _accountsRequester.GetAccountSummaries();
                return ar.Select(x => new KeyValuePair<string, string>(x.account.id, string.Format("{0} ({1})", x.account.alias, x.account.id))).ToList();
            }
        }

        private void Login()
        {

            MainWindow mw = new MainWindow();
            mw.Show();

            Application.Current.MainWindow = mw;

            CloseAction();
        }
    }
}