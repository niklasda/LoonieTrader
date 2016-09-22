using GalaSoft.MvvmLight;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel(ISettings settings, IAccountsRequester accountsRequester)
        {
        }

    }
}