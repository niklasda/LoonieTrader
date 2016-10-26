using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public LoginWindowViewModel(ISettingsService settingsService, IAccountsRequester accountsRequester,
            IDialogService dialogService)
        {
            _settingsService = settingsService;
            _accountsRequester = accountsRequester;
            _dialogService = dialogService;

            LoginCommand = new RelayCommand(Login, () => IsInfoCompletedForLogin);
            ServerStatusCommand = new RelayCommand(OpenServerStatus);
            ReloadAccountsCommand = new RelayCommand(ReloadAccounts, () => IsInfoCompletedForAccountLoad);

            AvailableEnvironments = new[] { Environments.Practice, Environments.Live };

            IEnvironmentSettings settings = _settingsService.CachedSettings?.SelectedEnvironment;

            //settings.Environment = "asd";
            //settings.ApiKey = "123123-123123";
            //settings.DefaultAccountId = "Prima";
            //settings.FavouriteInstruments = new string[] {"SDF_SDF", "SDF_DFG"};
            //settings.UserId = "456456";

            //_settingsService.SaveSettings(settings);

            SelectedEnvironmentKey = settings?.EnvironmentKey;
            ApiKey = settings?.ApiKey;


            if (IsInDesignMode)
            {
            }
            else
            {
                ReloadAccounts();

                if (AvailableAccounts != null)
                {
                    var prim =
                        AvailableAccounts.FirstOrDefault(
                            x => x.Value.StartsWith("Primary ", StringComparison.CurrentCultureIgnoreCase));
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

        public ObservableCollection<KeyValuePair<string, string>> AvailableAccounts { get; set; } =
            new ObservableCollection<KeyValuePair<string, string>>();

        public ICommand ReloadAccountsCommand { get; set; }

        private void Login()
        {
            _settingsService.CachedSettings.SelectedEnvironment.DefaultAccountId = SelectedAccountKey;
            _settingsService.SaveSettings(_settingsService.CachedSettings);

            MainWindow mw = new MainWindow();
            mw.Show();

            Application.Current.MainWindow = mw;

            CloseAction();
        }

        private void ReloadAccounts()
        {
            try
            {
                AvailableAccounts.Clear();

                if (IsInfoCompletedForAccountLoad)
                {
                   // EnvironmentKeys enk;
                   // if (Enum.TryParse(SelectedEnvironmentKey, out enk))
                   // {

 //                   }
                    _settingsService.CachedSettings.SelectedEnvironmentKey = SelectedEnvironmentKey;
//                        EnvironmentKeys.TryParse(SelectedEnvironmentKey);
                    _settingsService.CachedSettings.SelectedEnvironment.ApiKey = ApiKey;

                    var ar = _accountsRequester.GetAccountSummaries();

                    foreach (var asr in ar)
                    {
                        AvailableAccounts.Add(new KeyValuePair<string, string>(asr.account.id,
                            string.Format("{0} ({1})", asr.account.alias, asr.account.id)));
                    }
//                    AvailableAccounts = new ObservableCollection<KeyValuePair<string, string>>(ar.Select(x => new KeyValuePair<string, string>(x.account.id, string.Format("{0} ({1})", x.account.alias, x.account.id))).ToArray()); 
                }
                else
                {
                    _dialogService.WarnOk("Please enter key and select environment");
                }
            }
            catch (Exception ex)
            {
                AvailableAccounts.Clear();
                _dialogService.WarnOk(string.Format("Failure to load accounts:{0}{1}", Environment.NewLine, ex.Message));
//                RaisePropertyChanged(() => AvailableAccounts);
            }
        }

        private void OpenServerStatus()
        {
            var ssw = new ServiceStatusWindow();
            ssw.Show();
        }
    }
}