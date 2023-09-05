using System.Collections.Generic;

namespace LoonieTrader.Library.Constants
{
    public static class Environments
    {
        public static KeyValuePair<string, string> Practice { get; } = new KeyValuePair<string, string>("Practice", "api-fxpractice");
        public static KeyValuePair<string, string> Live { get; } = new KeyValuePair<string, string>("Live", "api-fxtrade");

        public static KeyValuePair<string, string> PracticeStreaming { get; } = new KeyValuePair<string, string>("PracticeStreaming", "stream-fxpractice");
        public static KeyValuePair<string, string> LiveStreaming { get; } = new KeyValuePair<string, string>("LiveStreaming", "stream-fxtrade");

     //   public static KeyValuePair<string, string> Status { get; } = new KeyValuePair<string, string>("Status", "api-status");

        public static string GetHostValueFor(string key)
        {
            if (key == "Live")
            {
                return Live.Value;
            }

            return Practice.Value;
        }

        public static string GetStreamingHostValueFor(string key)
        {
            if (key == "Live")
            {
                return LiveStreaming.Value;
            }

            return PracticeStreaming.Value;

        }
    }
}