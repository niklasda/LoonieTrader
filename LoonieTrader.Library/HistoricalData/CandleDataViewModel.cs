using System;

namespace LoonieTrader.Library.HistoricalData
{
    public class CandleDataViewModel
    {
        public string Ticker { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public DateTime DatePlusTime
        {
            get
            {
                
                var dt = DateTime.ParseExact(string.Format("{0} {1}", Date, Time), "yyyyMMdd HHmmss", null);
                //Console.WriteLine("{0} - {1} - {2} - {3}", dt.ToString("G"), dt.Ticks, TimeSpan.FromHours(1).Ticks, dt.Ticks/TimeSpan.FromHours(1).Ticks);

                // 2016-10-27 11:50:41 - 636131658410000000 - 36000000000 - 17670323
                // 636131658410000000 / 36000000000 = 17670323,844722222222222222222222
                // 636131658420000000 / 36000000000 = 17670323,845
                return dt;
            }
        }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public int Volume { get; set; }
    }
    //<TICKER>,<DTYYYYMMDD>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>
}