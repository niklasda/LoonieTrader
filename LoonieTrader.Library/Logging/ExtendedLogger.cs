using System;
using System.Threading;
using LoonieTrader.Library.Interfaces;
using Serilog;
using Serilog.Events;

namespace LoonieTrader.Library.Logging
{
    public class ExtendedLogger : IExtendedLogger
    {
        public ExtendedLogger(ILogger logger)
        {
            _logger = logger;
        }

        private readonly ILogger _logger;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Debug, null, args.Length > 0 ? string.Format(message, args) : message);
            AddToLogCache(l);
        }

        public void Information(string message, params object[] args)
        {
            _logger.Information(message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Information, null, args.Length > 0 ? string.Format(message, args) : message);
            AddToLogCache(l);
        }

        public void Warning(Exception exception, string message, params object[] args)
        {
            _logger.Warning(exception, message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Warning, exception, args.Length > 0 ? string.Format(message, args) : message);
            AddToLogCache(l);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);

            var l = new LogEntry(DateTime.Now, LogEventLevel.Error, exception, args.Length > 0 ? string.Format(message, args) : message);
            AddToLogCache(l);
        }

        private void AddToLogCache(LogEntry l)
        {
            if (_uiContext != null)
            {
                _uiContext.Post(o => LogCache.LogEntries.Insert(0, l), null);
            }
            else
            {
                // unclear why this happens, but _uiContext is mostly null
                LogCache.LogEntries.Insert(0, l);
            }
        }
    }
}