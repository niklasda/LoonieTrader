using System;
using JetBrains.Annotations;

namespace LoonieTrader.Library.Interfaces;

public interface IExtendedLogger
{
    [StringFormatMethod("message")]
    void Information(string message, params object[] args);

    [StringFormatMethod("message")]
    void Warning(Exception exception, string message, params object[] args);

    [StringFormatMethod("message")]
    void Debug(string message, params object[] args);

    [StringFormatMethod("message")]
    void Error(Exception exception, string message, params object[] args);
}