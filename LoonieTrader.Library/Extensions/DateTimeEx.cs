using System;

namespace LoonieTrader.Library.Extensions
{
    public static class DateTimeEx
    {
        public static string ToRfc3339(this DateTime timestamp)
        {
            ////2014-07-02T04:00:00.000000Z   "2016-10-24T12:17:49.0439507+02:00"
            /// "2016-10-24T12:21:54.63000Z"
            //
            var timeString = timestamp.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
            return timeString;
        }
    }
}