using GalaSoft.MvvmLight;
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
        }

    }
}