using Oanda.RestLibrary.Interfaces;

namespace Oanda.RestLibrary.Configuration
{
    public class Settings : ISettings
    {
        public string Environment { get; set; }

        public string ApiKey { get; set; }

        public string DefaultAccount { get; set; }
    }
}