using System.Linq;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using YamlDotNet.Serialization;

namespace LoonieTrader.Library.Models
{
    public class Settings : ISettings
    {
        public string SelectedEnvironmentKey { get; set; }

        public EnvironmentSettings[] EnvironmentSettings { get; set; }

        [YamlIgnore]
        public IEnvironmentSettings SelectedEnvironment
        {
            get
            {
                var selectedEnv = EnvironmentSettings.SingleOrDefault(x => x.EnvironmentKey == SelectedEnvironmentKey);
                return selectedEnv ?? EnvironmentSettings.SingleOrDefault(x => x.EnvironmentKey == Environments.Practice.Key);
            }
        }

        [YamlIgnore]
        public static Settings Empty
        {
            get
            {
                return new Settings()
                {
                    EnvironmentSettings = new[]
                    {
                        new EnvironmentSettings() {EnvironmentKey = Environments.Practice.Key},
                        new EnvironmentSettings() {EnvironmentKey = Environments.Live.Key}
                    }
                };
            }
        }
    }

    public class EnvironmentSettings : IEnvironmentSettings
    {
        public string EnvironmentKey { get; set; }

        public string ApiKey { get; set; }

        public string DefaultAccountId { get; set; }

        public string[] FavouriteInstruments { get; set; }
    }
}