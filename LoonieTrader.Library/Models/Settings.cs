using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.Models
{
    public class Settings : ISettings
    {
        public string Environment { get; set; }

        public string ApiKey { get; set; }

        public string DefaultAccountId { get; set; }

        public string UserId { get; set; }

        public string[] FavouriteInstruments { get; set; }
    }
}