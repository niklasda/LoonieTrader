using System;
using System.Runtime.InteropServices.Expando;
using LoonieTrader.RestLibrary.Caches;
using LoonieTrader.RestLibrary.Interfaces;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace LoonieTrader.RestLibrary.Configuration
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