using System;

namespace LoonieTrader.Library.TimeFrames
{
    public static class TimeFrameFactory
    {
        public static TimeFrame CreateFromMinutes(int minutes)
        {
            return new TimeFrame($"{minutes} Minutes", TimeSpan.FromMinutes(minutes));
        }
    }
}