using System.Collections.ObjectModel;
using LoonieTrader.Library.Configuration;

namespace LoonieTrader.Library.Logging
{
    public static class LogCache
    {

        public static ObservableCollection<LogEntry> LogEntries { get; set; } = new ObservableCollection<LogEntry>();
    }
}