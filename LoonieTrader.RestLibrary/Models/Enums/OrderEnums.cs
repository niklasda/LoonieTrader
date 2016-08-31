// ReSharper disable InconsistentNaming
namespace LoonieTrader.RestLibrary.Models.Enums
{
    public enum OrderTypes
    {
        MARKET,
        LIMIT,
        STOP,
        MARKET_IF_TOUCHED,
        TAKE_PROFIT,
        STOP_LOSS,
        TRAILING_STOP_LOSS
    }

    public enum OrderState
    {
        PENDING,
        FILLED,
        TRIGGERED,
        CANCELLED
    }

    public enum TimeInForce
    {
        GTC,    // The Order is “Good unTil Cancelled”
        GTD,    // The Order is “Good unTil Date” and will be cancelled at the provided time
        GFD,    // The Order is “Good For Day” and will be cancelled at 5pm New York time
        FOK,    // The Order must be immediately “Filled Or Killed”
        IOC     // The Order must be “Immediatedly paritally filled Or Cancelled”
    }

    public enum OrderPositionFill
    {
        OPEN_ONLY,      // When the Order is filled, only allow Positions to be opened or extended.
        REDUCE_FIRST,   // When the Order is filled, always fully reduce an existing Position before opening a new Position.
        REDUCE_ONLY,    // When the Order is filled, only reduce an existing Position.
        DEFAULT,        // When the Order is filled, use REDUCE_FIRST behaviour for non-client hedging Accounts, and OPEN_ONLY behaviour for client hedging Accounts.
    }

}
