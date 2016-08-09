// ReSharper disable InconsistentNaming
namespace LoonieTrader.RestLibrary.Models
{
    public enum TradeState
    {
        OPEN,                   // The Trade is currently open
        CLOSED,                 // The Trade has been fully closed
        CLOSE_WHEN_TRADEABLE    // The Trade will be closed as soon as the trade’s instrument becomes tradeable
    }

}
