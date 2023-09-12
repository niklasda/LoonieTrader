namespace LoonieTrader.Library.Interfaces;

public interface IFileReaderWriterService
{
    void SaveLocalJson(string fileNamePart1, string fileNamePart2, string json);
    void SaveLocalJson(string fileNamePart1, string fileNamePart2, string fileNamePart3, string json);
    string GetLogFilePattern();
    string GetTestLogFilePattern();
    string GetHistoricalDataFolderPath();
}