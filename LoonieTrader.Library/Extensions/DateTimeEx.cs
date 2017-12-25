using System;

namespace LoonieTrader.Library.Extensions
{
    public static class DateTimeEx
    {
        public static string ToRfc3339(this DateTime timestamp)
        {
            // RFC 2014-07-02T04:00:00.000000Z
            // ISO 2016-10-24T12:17:49.0439507+02:00
            // RFC 2016-10-24T12:21:54.630000Z
            //
            var timeString = timestamp.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
            return timeString;
        }


        public static DateTime RoundDownTo(this DateTime timeStamp, TimeSpan length)
        {
            //    timeStamp.Hour = timeStamp.Hour % length.Hours;
            //  timeStamp.Minute = timeStamp.Minute % length.Minutes;
            //timeStamp.Second = timeStamp.Second % length.Seconds;


            var hmod = length.Hours == 0 ? 1 : length.Hours;
            var mmod = length.Minutes == 0 ? 1 : length.Minutes;
            var smod = length.Seconds == 0 ? 1 : length.Seconds;

            if (hmod > 0 && mmod == 0)
            {
                mmod = 60;
            }

            if (hmod > 0 || mmod > 0)
            {
                smod = 60;
            }

            var h = timeStamp.Hour % hmod;
            var m = timeStamp.Minute % mmod;
            var s = timeStamp.Second % smod;

            var roundedTimeStamp = new DateTime(timeStamp.Year, timeStamp.Month, timeStamp.Day, timeStamp.Hour - h, timeStamp.Minute - m, timeStamp.Second - s);

            return roundedTimeStamp;
        }

        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
    }
}