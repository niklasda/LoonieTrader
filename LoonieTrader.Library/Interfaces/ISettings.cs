using LoonieTrader.Library.Models;

namespace LoonieTrader.Library.Interfaces
{
    public interface ISettings
    {
        string SelectedEnvironmentKey { get; set; }

        EnvironmentSettings[] EnvironmentSettings { get; set; }

        IEnvironmentSettings SelectedEnvironment { get; }
    }

    public interface IEnvironmentSettings
    {
        string EnvironmentKey { get; set; }

        string ApiKey { get; set; }

        string DefaultAccountId { get; set; }

        string[] FavouriteInstruments { get; set; }
    }
}