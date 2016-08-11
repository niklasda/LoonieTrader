using System;

namespace LoonieTrader.RestLibrary.HistoricalData
{
    public class CandleDataType2ViewModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime DatePlusTime { get { return DateTime.ParseExact(string.Format("{0} {1}", Date, Time), "yyyy.MM.dd HH:mm", null); } }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public int Vol { get; set; }
    }
    //2016.08.11,01:00,1.1185,1.11861,1.1185,1.11856,1604
}