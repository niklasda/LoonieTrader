using GalaSoft.MvvmLight;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel(ISettings settings, IAccountsRequester accountsRequester)
        {
        }

    }
}