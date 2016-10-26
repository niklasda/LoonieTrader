using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.Services
{
    public class SettingserService : ISettingsService
    {
        public SettingserService(IFileReaderWriterService fileReaderWriter)
        {
            _fileReaderWriter = fileReaderWriter;
        }

        private readonly IFileReaderWriterService _fileReaderWriter;
        private ISettings2 _lastSettings;

        public ISettings2 CachedSettings
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

        public ISettings2 LoadSettings()
        {
            return _fileReaderWriter.LoadConfiguration();
        }

        public void SaveSettings(ISettings2 settings)
        {
            _fileReaderWriter.SaveConfiguration(settings);
        }
    }
}