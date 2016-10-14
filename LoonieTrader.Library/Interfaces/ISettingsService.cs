namespace LoonieTrader.Library.Interfaces
{
    public interface ISettingsService
    {
        ISettings CachedSettings { get; }
        ISettings LoadSettings();
        void SaveSettings(ISettings settings);
    }
}