using System.Collections.Generic;

namespace LoonieTrader.Library.Constants
{
    public static class Environments
    {
        public static KeyValuePair<string, string> Practice { get; } = new KeyValuePair<string, string>("Practice", "api-fxpractice");
        public static KeyValuePair<string, string> Live { get; } = new KeyValuePair<string, string>("Live", "api-fxtrade");

        public static KeyValuePair<string, string> PracticeStreaming { get; } = new KeyValuePair<string, string>("PracticeStreaming", "stream-fxpractice");
    }
}