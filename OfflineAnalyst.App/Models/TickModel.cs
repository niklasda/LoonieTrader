using System;

namespace OfflineAnalyst.App.Models
{
    public class TickModel
    {
        public int Milli { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public decimal AskVolume { get; set; }
        public decimal BidVolume { get; set; }
      //  public int Sec { get; set; }

        public override string ToString()
        {
            return $"{TimeStamp.ToString("HH:mm:ss.fff")}: ask:{Ask}, bid:{Bid}, av:{AskVolume}, bv:{BidVolume}";
        }
    }
}