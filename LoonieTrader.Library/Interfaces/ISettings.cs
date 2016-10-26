using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Models;

namespace LoonieTrader.Library.Interfaces
{
    public interface ISettings2
    {
        string SelectedEnvironmentKey { get; set; }

        EnvironmentSettings[] EnvironmentSettings { get; set; }

        IEnvironmentSettings2 SelectedEnvironment { get; }
    }

    public interface IEnvironmentSettings2
    {
        string EnvironmentKey { get; set; }

        string ApiKey { get; set; }

        string DefaultAccountId { get; set; }

        string[] FavouriteInstruments { get; set; }
    }
}