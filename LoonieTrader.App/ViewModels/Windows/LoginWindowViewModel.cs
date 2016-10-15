using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.App.Views;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class LoginWindowViewModel : ViewModelBase
    {
        public LoginWindowViewModel(ISettingsService settingsService, IAccountsRequester accountsRequester, IDialogService dialogService)
        {
            _settingsService = settingsService;
            _accountsRequester = accountsRequester;
            _dialogService = dialogService;

            LoginCommand = new RelayCommand(Login, () => IsInfoCompletedForLogin);
            ServerStatusCommand = new RelayCommand(OpenServerStatus);
            ReloadAccountsCommand = new RelayCommand(ReloadAccounts, () => IsInfoCompletedForAccountLoad);

            ISettings settings = _settingsService.CachedSettings;

            //settings.Environment = "asd";
            //settings.ApiKey = "123123-123123";
            //settings.DefaultAccountId = "Prima";
            //settings.FavouriteInstruments = new string[] {"SDF_SDF", "SDF_DFG"};
            //settings.UserId = "456456";

            //_settingsService.SaveSettings(settings);

            ApiKey = settings.ApiKey;
            SelectedEnvironmentKey = settings.Environment;

            AvailableEnvironments = new[] { Environments.Practice, Environments.Live };

            if (IsInDesignMode)
            {

            }
            else
            {
                ReloadAccounts();

                if (AvailableAccounts != null)
                {
                    var prim = AvailableAccounts.FirstOrDefault(x => x.Value.StartsWith("Primary ", StringComparison.CurrentCultureIgnoreCase));
                    if (prim.Key != null)
                    {
                        SelectedAccountKey = prim.Key;
                    }
                }
            }
        }

        private readonly ISettingsService _settingsService;
        private readonly IAccountsRequester _accountsRequester;
        private readonly IDialogService _dialogService;

        private bool IsInfoCompletedForLogin
        {
            get
            {
                return IsInfoCompletedForAccountLoad
                    && !string.IsNullOrWhiteSpace(SelectedAccountKey);
            }
        }
        private bool IsInfoCompletedForAccountLoad
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

        public IList<KeyValuePair<string, string>> AvailableEnvironments { get; private set; }

        public ICommand LoginCommand { get; set; }

        public ICommand ServerStatusCommand { get; set; }

        public Action CloseAction { get; set; }

        public IList<KeyValuePair<string, string>> AvailableAccounts { get; set; }

        public ICommand ReloadAccountsCommand { get; set; }

        private void Login()
        {
            MainWindow mw = new MainWindow();
            mw.Show();

            Application.Current.MainWindow = mw;

            CloseAction();
        }

        private void ReloadAccounts()
        {
            try
            {
                if (IsInfoCompletedForAccountLoad)
                {
                    var ar = _accountsRequester.GetAccountSummaries();
                    AvailableAccounts = ar.Select(x => new KeyValuePair<string, string>(x.account.id, string.Format("{0} ({1})", x.account.alias, x.account.id))).ToArray();
                }
                else
                {
                    _dialogService.WarnOk("Please enter key and select environment");
                }
            }
            catch (Exception ex)
            {
                _dialogService.WarnOk(string.Format("Failure to load accounts:{0}{1}", Environment.NewLine, ex.Message));
                RaisePropertyChanged(() => AvailableAccounts);
            }
        }

        private void OpenServerStatus()
        {
            var ssw = new ServiceStatusWindow();
            ssw.Show();
        }
    }
}