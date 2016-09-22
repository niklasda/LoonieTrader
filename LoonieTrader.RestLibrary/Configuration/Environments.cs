using System.Collections.Generic;

namespace LoonieTrader.Library.Configuration
{
    public static class Environments
    {
        public static KeyValuePair<string, string> Practice { get; } = new KeyValuePair<string, string>("Practice", "api-fxpractice");
        public static KeyValuePair<string, string> Live { get; } = new KeyValuePair<string, string>("Live", "api-fxtrade");
    }
}