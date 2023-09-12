using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using Microsoft.Extensions.Configuration;

namespace LoonieTrader.Library.Services;

public class SettingsService : ISettingsService
{
    public SettingsService()
    {
        CachedSettings = LoadSettings();
    }


    public ISettings CachedSettings { get; }
                

                

    private ISettings LoadSettings()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", false);

        var config = configuration.Build();
        ISettings settings = config.Get<Settings>();
        return settings;

        //   return _fileReaderWriter.LoadConfiguration();
    }

}