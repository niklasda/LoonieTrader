using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Logging;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class LogWindowViewModel : ViewModelBase
    {
        public ObservableCollection<LogEntry> LogEntries
        {
            get { return LogCache.LogEntries; }
        }
    }
}