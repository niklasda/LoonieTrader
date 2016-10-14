using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.Services
{
    public class SettingserService : ISettingsService
    {
        public SettingserService( IFileReaderWriterService fileReaderWriter)
        {
            _fileReaderWriter = fileReaderWriter;
        }

        private readonly IFileReaderWriterService _fileReaderWriter;
        private ISettings _lastSettings;

        public ISettings CachedSettings
        {
            get
            {
                if (_lastSettings == null)
                {
                    _lastSettings = LoadSettings();
                }

                return _lastSettings;
            }
        }

        public ISettings LoadSettings()
        {
            return _fileReaderWriter.LoadConfiguration();
        }

        public void SaveSettings(ISettings settings)
        {
            _fileReaderWriter.SaveConfiguration(settings);
        }
    }
}