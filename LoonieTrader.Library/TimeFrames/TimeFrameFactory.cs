using System;

namespace LoonieTrader.Library.TimeFrames
{
    public static class TimeFrameFactory
    {
        public static TimeFrame Create15Minutes()
        {
            return new TimeFrame("15 Minutes", TimeSpan.FromMinutes(15));
        }
    }
}