using System;

namespace LoonieTrader.Shared.Models
{
    public class OhlciPoint : IOhlciPoint
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public DateTime Timestamp { get; set; }
        public long XIndex { get; set; }

        public static OhlciPoint Empty { get { return new OhlciPoint(); } }
    }

}