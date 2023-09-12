//using System;

//namespace LoonieTrader.Library.Models
//{
//    public class OhlcModel
//    {
//        public string Date { get; set; }

//        public string Time { get; set; }

//        public DateTime DatePlusTime
//        {
//            get
//            {
                
//                var dt = DateTime.ParseExact(string.Format("{0} {1}", Date, Time), "yyyyMMdd HHmmss", null);
//                //Console.WriteLine("{0} - {1} - {2} - {3}", dt.ToString("G"), dt.Ticks, TimeSpan.FromHours(1).Ticks, dt.Ticks/TimeSpan.FromHours(1).Ticks);

//                // 2016-10-27 11:50:41 - 636131658410000000 - 36000000000 - 17670323
//                // 636131658410000000 / 36000000000 = 17670323,844722222222222222222222
//                // 636131658420000000 / 36000000000 = 17670323,845
//                return dt;
//            }
//            set
//            {
//                Date = value.ToString("yyyyMMdd");
//                Time = value.ToString("HHmmss");
                
//            }
//        }

//        public double Open { get; set; }

//        public double High { get; set; }

//        public double Low { get; set; }

//        public double Close { get; set; }

//        public int TickCount { get; set; }

//        public double Volume { get; set; }
//    }
//    //<TICKER>,<DTYYYYMMDD>,<TIME>,<OPEN>,<HIGH>,<LOW>,<CLOSE>,<VOL>
//}