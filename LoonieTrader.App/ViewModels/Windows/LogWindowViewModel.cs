﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using LoonieTrader.Library.Logging;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LogWindowViewModel : ViewModelBase
    {

        public ObservableCollection<LogEntry> LogEntries
        {
            get { return LogCache.LogEntries; }
        }
    }
}