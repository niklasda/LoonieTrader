using System.Windows;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Constants;

namespace LoonieTrader.App.UiServices
{
    [UsedImplicitly]
    public class DialogService : IDialogService
    {
        public bool AskYesNo(string message)
        {
            var res = MessageBox.Show(message, AppProperties.ApplicationName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            return res == MessageBoxResult.Yes;
        }

        public void WarnOk(string message)
        {
            MessageBox.Show(message, AppProperties.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}