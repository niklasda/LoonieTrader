using System;
using Serilog.Events;

namespace LoonieTrader.Library.Logging
{
    public class LogEntry
    {
        public LogEntry(DateTime timestamp, LogEventLevel level, Exception exception, string message)
        {
            Timestamp = timestamp;
            Level = level;
            Exception = exception;
            Message = message;
        }

        public DateTime Timestamp { get; private set; }
        public LogEventLevel Level { get; private set; }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }
    }
}