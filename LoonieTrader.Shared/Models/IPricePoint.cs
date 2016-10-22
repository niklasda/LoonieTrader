using System;

namespace LoonieTrader.Shared.Models
{
    public interface IPricePoint
    {
        decimal Price { get; set; }
        DateTime Timestamp { get; set; }
        long XIndex { get; set; }

    }
}