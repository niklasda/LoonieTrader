using System;

namespace LoonieTrader.Shared.Models
{
    public interface IOhlcPoint
    {
        decimal Open { get; set; }
        decimal High { get; set; }
        decimal Low { get; set; }
        decimal Close { get; set; }
        DateTime Timestamp { get; set; }
        long XIndex { get; set; }

    }

}