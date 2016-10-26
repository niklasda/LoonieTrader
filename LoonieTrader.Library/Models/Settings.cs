using System.Linq;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.Models
{
    public class Settings : ISettings2
    {
        public string SelectedEnvironmentKey { get; set; }

        public EnvironmentSettings[] EnvironmentSettings { get; set; }

        public IEnvironmentSettings2 SelectedEnvironment
        {
            get { return EnvironmentSettings.SingleOrDefault(x => x.EnvironmentKey == SelectedEnvironmentKey); }
        }
    }

    public class EnvironmentSettings : IEnvironmentSettings2
    {
        public string EnvironmentKey { get; set; }

        public string ApiKey { get; set; }

        public string DefaultAccountId { get; set; }
        
        public string[] FavouriteInstruments { get; set; }
    }
}