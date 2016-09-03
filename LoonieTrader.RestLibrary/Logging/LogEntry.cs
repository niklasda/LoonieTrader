using System;
using Serilog.Events;

namespace LoonieTrader.RestLibrary.Logging
{
    public class LogEntry
    {
        public LogEntry(DateTime timeStamp, LogEventLevel level, Exception exception, string message)
        {
            TimeStamp = timeStamp;
            Level = level;
            Exception = exception;
            Message = message;
        }

        public DateTime TimeStamp { get; private set; }
        public LogEventLevel Level { get; private set; }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }
    }
}