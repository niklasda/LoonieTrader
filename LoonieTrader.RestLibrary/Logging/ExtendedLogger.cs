using System;
using LoonieTrader.Library.Interfaces;
using Serilog;
using Serilog.Events;

namespace LoonieTrader.Library.Logging
{
    public class ExtendedLogger : IExtendedLogger
    {
        private readonly ILogger _logger;

        public ExtendedLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Debug, null, args.Length >0 ? string.Format(message, args) : message);
            LogCache.LogEntries.Insert(0, l);
        }

        public void Information(string message, params object[] args)
        {
            _logger.Information(message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Information, null, args.Length > 0 ? string.Format(message, args) : message);
            LogCache.LogEntries.Insert(0, l);
        }

        public void Warning(Exception exception, string message, params object[] args)
        {
            _logger.Warning(exception,  message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Warning, exception, args.Length > 0 ? string.Format(message, args) : message);
            LogCache.LogEntries.Insert(0, l);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Error, exception, args.Length > 0 ? string.Format(message, args) : message);
            LogCache.LogEntries.Insert(0, l);
        }
    }
}