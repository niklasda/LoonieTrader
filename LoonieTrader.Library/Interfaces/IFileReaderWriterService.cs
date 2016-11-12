namespace LoonieTrader.Library.Interfaces
{
    public interface IFileReaderWriterService
    {
        ISettings LoadConfiguration();
        void SaveConfiguration(ISettings settings);
        string LoadLocalJson(string fileNamePart1, string fileNamePart2);
        void SaveLocalJson(string fileNamePart1, string fileNamePart2, string json);
        void SaveLocalJson(string fileNamePart1, string fileNamePart2, string fileNamePart3, string json);
        string GetLocalFolderPath();
        string GetLogFilePattern();
        string GetIndicatorFolderPath();
        string GetHistoricalDataFolderPath();
        string GetLayoutFolderPath();
    }
}