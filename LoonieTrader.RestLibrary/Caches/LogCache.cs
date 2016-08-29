using System.Collections.ObjectModel;
using LoonieTrader.RestLibrary.Configuration;

namespace LoonieTrader.RestLibrary.Caches
{
    public static class LogCache
    {

        public static ObservableCollection<LogEntry> LogEntries { get; set; } = new ObservableCollection<LogEntry>();
    }
}