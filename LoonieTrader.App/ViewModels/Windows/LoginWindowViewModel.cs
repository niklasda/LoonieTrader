using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Configuration;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public LoginWindowViewModel(ISettings settings, IAccountsRequester accountsRequester)
        {
            _accountsRequester = accountsRequester;
            LoginCommand = new RelayCommand(Login, () => CanClose);

            ApiKey = settings.ApiKey;
            SelectedEnvironmentKey = settings.Environment;

            _availableEnvironments =  new[] { Environments.Practice, Environments.Live };

            var ar = _accountsRequester.GetAccountSummaries();
            _availableAccounts = ar.Select(x => new KeyValuePair<string, string>(x.account.id, string.Format("{0} ({1})", x.account.alias, x.account.id))).ToArray();

            var prim = _availableAccounts.FirstOrDefault(x => x.Value.StartsWith("Primary ", StringComparison.CurrentCultureIgnoreCase));
            if (prim.Key != null)
            {
                SelectedAccountKey = prim.Key;
            }
        }

        private readonly IAccountsRequester _accountsRequester;
        private KeyValuePair<string, string>[] _availableEnvironments;
        private KeyValuePair<string, string>[] _availableAccounts;

        public bool CanClose
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedEnvironmentKey)
                    && !string.IsNullOrWhiteSpace(ApiKey)
                    && !string.IsNullOrWhiteSpace(SelectedAccountKey)
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
            get { return _availableAccounts; }
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