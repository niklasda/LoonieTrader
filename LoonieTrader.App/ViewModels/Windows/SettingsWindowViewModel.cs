using System;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel(ISettingsService settingsService, IAccountsRequester accountsRequester)
        {
            _settingsService = settingsService;
            ISettings settings = _settingsService.CachedSettings;

            var serverCulture = new CultureInfo("en-US");

            ClientCultureSetting = CultureInfo.CurrentCulture.DisplayName;
            ClientUiCultureSetting =  CultureInfo.CurrentUICulture.DisplayName;
            ServerCultureSetting = serverCulture.DisplayName;

            IsEnableOneClickTrading = true;
            IsRequireProfitLoss = false;

            IsDefaultProfitDistance = false;
            DefaultProfitDistance = 10;
            IsDefaultLossDistance = false;
            DefaultLossDistance = 10;
            IsDefaultTrailingDistance = false;
            DefaultTrailingDistance = 10;

            SaveCommand = new RelayCommand(SaveAndClose);
        }

        private readonly ISettingsService _settingsService;

        public string ClientCultureSetting { get; set; }
        public string ClientUiCultureSetting { get; set; }
        public string ServerCultureSetting { get; set; }

        public ICommand SaveCommand {get; set; }

        private bool _isEnableOneClickTrading;
        public bool IsEnableOneClickTrading
        {
            get { return _isEnableOneClickTrading; }
            set
            {
                if (_isEnableOneClickTrading != value)
                {
                    _isEnableOneClickTrading = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isRequireProfitLoss;
        public bool IsRequireProfitLoss
        {
            get { return _isRequireProfitLoss; }
            set
            {
                if (_isRequireProfitLoss != value)
                {
                    _isRequireProfitLoss = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isDefaultProfitDistance;
        public bool IsDefaultProfitDistance
        {
            get { return _isDefaultProfitDistance; }
            set
            {
                if (_isDefaultProfitDistance != value)
                {
                    _isDefaultProfitDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _defaultProfitDistance;
        public int DefaultProfitDistance
        {
            get { return _defaultProfitDistance; }
            set
            {
                if (_defaultProfitDistance != value)
                {
                    _defaultProfitDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isDefaultLossDistance;
        public bool IsDefaultLossDistance
        {
            get { return _isDefaultLossDistance; }
            set
            {
                if (_isDefaultLossDistance != value)
                {
                    _isDefaultLossDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _defaultLossDistance;
        public int DefaultLossDistance
        {
            get { return _defaultLossDistance; }
            set
            {
                if (_defaultLossDistance != value)
                {
                    _defaultLossDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _isDefaultTrailingDistance;
        public bool IsDefaultTrailingDistance
        {
            get { return _isDefaultTrailingDistance; }
            set
            {
                if (_isDefaultTrailingDistance != value)
                {
                    _isDefaultTrailingDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _defaultTrailingDistance;
        public int DefaultTrailingDistance
        {
            get { return _defaultTrailingDistance; }
            set
            {
                if (_defaultTrailingDistance != value)
                {
                    _defaultTrailingDistance = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void SaveAndClose()
        {
            // _settingsService.SaveSettings();

            CloseWindow.Invoke();
        }

        public Action CloseWindow { get; set; }
    }
}