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
            _settings = settingsService.CachedSettings;
            _accountsRequester = accountsRequester;
            _dialogService = dialogService;

            LoginCommand = new RelayCommand(Login, () => IsInfoCompletedForLogin);
            ServerStatusCommand = new RelayCommand(OpenServerStatus);
            ReloadAccountsCommand = new RelayCommand(ReloadAccounts, () => IsInfoCompletedForAccountLoad);

            AvailableEnvironments = new[] {Environments.Practice, Environments.Live};

            SelectedEnvironmentKey = _settings.SelectedEnvironmentKey;
            ApiKey = _settings.SelectedEnvironment.ApiKey;


            if (IsInDesignMode)
            {
            }
            else
            {
                LoadAccounts();
            }
        }

        private readonly ISettingsService _settingsService;
        private readonly ISettings _settings;
        private readonly IAccountsRequester _accountsRequester;
        private readonly IDialogService _dialogService;

        private bool IsInfoCompletedForLogin
        {
            get { return IsInfoCompletedForAccountLoad && !string.IsNullOrWhiteSpace(SelectedAccountKey); }
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

        public string ApiKey
        {
            get { return _settings.SelectedEnvironment.ApiKey; }
            set
            {
                if (_settings.SelectedEnvironment.ApiKey != value)
                {
                    _settings.SelectedEnvironment.ApiKey = value;

                    AvailableAccounts.Clear();

                    RaisePropertyChanged();
                }
            }
        }

        public string SelectedEnvironmentKey
        {
            get { return _settings.SelectedEnvironmentKey; }
            set
            {
                if (_settings.SelectedEnvironmentKey != value)
                {
                    _settings.SelectedEnvironmentKey = value;

                    SelectedAccountKey = null;
                    AvailableAccounts.Clear();

                    RaisePropertyChanged();
                    RaisePropertyChanged(() => ApiKey);
                }
            }
        }

        public string SelectedAccountKey
        {
            get { return _settings.SelectedEnvironment.DefaultAccountId; }
            set
            {
                if (_settings.SelectedEnvironment.DefaultAccountId != value)
                {
                    _settings.SelectedEnvironment.DefaultAccountId = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IList<KeyValuePair<string, string>> AvailableEnvironments { get; private set; }

        public ICommand LoginCommand { get; set; }

        public ICommand ServerStatusCommand { get; set; }

        public Action CloseAction { get; set; }

        public ObservableCollection<KeyValuePair<string, string>> AvailableAccounts { get; set; } = new ObservableCollection<KeyValuePair<string, string>>();

        public ICommand ReloadAccountsCommand { get; set; }

        private void Login()
        {
            _settingsService.SaveSettings(_settings);

            MainWindow mw = new MainWindow();
            mw.Show();

            Application.Current.MainWindow = mw;

            CloseAction();
        }

        private void LoadAccounts()
        {
            AvailableAccounts.Clear();

            if (IsInfoCompletedForAccountLoad)
            {
                ReloadAccounts();
            }
        }

        private void ReloadAccounts()
        {
           // try
           // {
                AvailableAccounts.Clear();

                if (IsInfoCompletedForAccountLoad)
                {
                    var ar = _accountsRequester.GetAccountSummaries();
                    if (!ar.Any())
                    {
                        AvailableAccounts.Clear();
                        SelectedAccountKey = null;
                        _dialogService.WarnOk("Failure to load accounts!");
                        return;
                    }

                    foreach (var asr in ar)
                    {
                        AvailableAccounts.Add(new KeyValuePair<string, string>(asr.account.id, string.Format("{0} ({1})", asr.account.alias, asr.account.id)));
                    }

                    SelectPrimaryAccount();
                }
                else
                {
                    _dialogService.WarnOk("Please enter key and select environment");
                }
            //}
            //catch (Exception ex)
           // {
           //     _dialogService.WarnOk(string.Format("Failure to load accounts:{0}{1}", Environment.NewLine, ex.Message));
           // }
        }

        private void SelectPrimaryAccount()
        {
            if (AvailableAccounts != null)
            {
                var prim = AvailableAccounts.FirstOrDefault(x => x.Value.StartsWith("Primary ", StringComparison.CurrentCultureIgnoreCase));
                if (prim.Key != null)
                {
                    SelectedAccountKey = prim.Key;
                }
            }
        }

        private void OpenServerStatus()
        {
            var ssw = new ServiceStatusWindow();
            ssw.Owner = Application.Current.MainWindow;
            ssw.Show();
        }
    }
}