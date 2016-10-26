namespace LoonieTrader.Library.Interfaces
{
    public interface ISettingsService
    {
        ISettings2 CachedSettings { get; }
        ISettings2 LoadSettings();
        void SaveSettings(ISettings2 settings);
    }
}