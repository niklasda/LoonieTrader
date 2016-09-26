using System;

namespace LoonieTrader.Library.HistoricalData
{
    public class CandleDataViewModel
    {
        public string Ticker { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime DatePlusTime { get { return DateTime.ParseExact(string.Format("{0} {1}", Date, Time), "yyyyMMdd HHmmss", null); } }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public int Volume { get; set; }
    }
    //<TICKER>,<DTYYYYMMDD>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>
}