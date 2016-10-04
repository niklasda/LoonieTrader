using System.Windows;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Constants;

namespace LoonieTrader.App.UiServices
{
    public class DialogService : IDialogService
    {
        public bool AskYesNo(string message)
        {
            var res = MessageBox.Show(message, AppProperties.ApplicationName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            return res == MessageBoxResult.Yes;
        }
    }
}