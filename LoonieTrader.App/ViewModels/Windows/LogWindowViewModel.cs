using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Logging;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class LogWindowViewModel : ViewModelBase
    {
        public LogWindowViewModel()
        {
            _logEntries = new ObservableCollection<LogEntry>(LogCache.LogEntries);
            LogCache.LogEntries.CollectionChanged += LogEntries_CollectionChanged;
        }

        private readonly ObservableCollection<LogEntry> _logEntries;

        public ObservableCollection<LogEntry> LogEntries => _logEntries;

        private void LogEntries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _logEntries.Insert(0, item as LogEntry);
                    }));
                }
            }
        }
    }
}