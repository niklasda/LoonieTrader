using System;
using System.Collections.Generic;
using System.Linq;
using LoonieTrader.Library.Enums;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Models;

namespace LoonieTrader.Library.TimeFrames
{
    public class TimeFrame
    {
        public TimeFrame(string name, TimeSpan length)
        {
            Name = name;
            Length = length;
        }

        public string Name { get; set; }
        public TimeSpan Length { get; }
        public TimeSpan Lenth { get; set; }

        public OhlcListModel ConvertTime(TickListModel tickList, PricePointType pointType)
        {
            IDictionary<DateTime, OhlcModel> dic = new Dictionary<DateTime, OhlcModel>();

            IList<TickModel> ticks = tickList.TickList;

            foreach (var tick in ticks)
            {
                var ts = tick.TimeStamp.RoundDownTo(Length);

                OhlcModel cd ;//= new OhlcModel();

                if (dic.TryGetValue(ts, out OhlcModel value))
                {
                    cd = value;
                }
                else
                {
                    cd = new OhlcModel();
                  //  cd.PointType = pointType;
                  //  cd.Ticker = tickList.Ticker;
                    cd.Date = ts.ToString("yyyyMMdd");
                    cd.Time = ts.ToString("HHmmss");
                    dic.Add(ts, cd);

                    if (pointType == PricePointType.Ask)
                    {
                        cd.Open = (double)tick.Ask;
                    }
                    else
                    {
                        cd.Open = (double)tick.Bid;
                    }
                }

                if (pointType == PricePointType.Ask)
                {
                    cd.High = Math.Max(cd.High, (double)tick.Ask);
                    cd.Low = Math.Min(cd.High, (double)tick.Ask);
                    cd.Close = (double)tick.Ask;
                    cd.Volume += (double)tick.AskVolume;

                }
                else
                {
                    cd.High = Math.Max(cd.High, (double)tick.Bid);
                    cd.Low = Math.Min(cd.High, (double)tick.Bid);
                    cd.Close = (double)tick.Bid;
                    cd.Volume += (double)tick.BidVolume;
                }

                cd.TickCount++;
            }

            OhlcListModel ohlcList = new OhlcListModel();
            ohlcList.PointType = pointType;
            ohlcList.Ticker = tickList.Ticker;
            ohlcList.OhlcList = dic.Values.ToList();

            return ohlcList;
        }
    }
}