using System;

namespace LoonieTrader.Shared.Models
{
    public class PricePoint : IPricePoint
    {
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
        public long XIndex { get; set; }

        public static PricePoint Empty { get { return new PricePoint(); } }

    }
}