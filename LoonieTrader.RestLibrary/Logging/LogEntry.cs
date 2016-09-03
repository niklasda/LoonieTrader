using System;
using Serilog.Events;

namespace LoonieTrader.RestLibrary.Logging
{
    public class LogEntry
    {
        public LogEntry( LogEventLevel level, Exception exception, string message)
        {
            Level = level;
            Exception = exception;
            Message = message;
        }

        public LogEventLevel Level { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}