using System.Collections.Specialized;
using System.Linq;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.Models
{
    public class Settings : ISettings
    {
        public string SelectedEnvironmentKey { get; set; }

        public EnvironmentSettings[] EnvironmentSettings { get; set; }

        public IEnvironmentSettings SelectedEnvironment
        {
            get
            {
                var selectedEnv = EnvironmentSettings.SingleOrDefault(x => x.EnvironmentKey == SelectedEnvironmentKey);
                return selectedEnv ?? EnvironmentSettings.SingleOrDefault(x => x.EnvironmentKey == Environments.Practice.Key);
            }
        }

    }

    public class EnvironmentSettings : IEnvironmentSettings
    {
        public string EnvironmentKey { get; set; }

        public string ApiKey { get; set; }

        public string DefaultAccountId { get; set; }

        public StringCollection FavoriteInstruments { get; set; } = new StringCollection();
    }
}