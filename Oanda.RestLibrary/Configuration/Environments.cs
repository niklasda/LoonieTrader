using System.Collections.Generic;

namespace LoonieTrader.RestLibrary.Configuration
{
    public static class Environments
    {
        public static KeyValuePair<string, string> Sandbox { get; } = new KeyValuePair<string, string>("Sandbox", "api-sandbox");
        public static KeyValuePair<string, string> Practice { get; } = new KeyValuePair<string, string>("Practice", "api-fxpractice");
        public static KeyValuePair<string, string> Live { get; } = new KeyValuePair<string, string>("Live", "api-fxtrade");
    }
}