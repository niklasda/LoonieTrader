using System;

namespace LoonieTrader.Library.Interfaces
{
    public interface IExtendedLogger
    {
        void Information(string message, params object[] args);
        void Warning(Exception exception, string message, params object[] args);
        void Debug(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
    }
}