using System;
using System.IO;
using System.Text;
using LoonieTrader.Library.Interfaces;
//using YamlDotNet.Serialization;
//using YamlDotNet.Serialization.NamingConventions;
//using Settings = LoonieTrader.Library.Models.Settings;

namespace LoonieTrader.Library.Services;

public class FileReaderWriterService : IFileReaderWriterService
{
    public FileReaderWriterService()
    {
        _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string dataPath = Path.Combine(_appDataFolderPath, FolderName, DataFolderName);
        string historicalDataPath = Path.Combine(_appDataFolderPath, FolderName, HistoricalDataFolderName);

        Directory.CreateDirectory(dataPath);
        Directory.CreateDirectory(historicalDataPath);
    }

    private const string FolderName = "LoonieTrader";
    private const string DataFolderName = "Data";
    private const string HistoricalDataFolderName = "HistoricalData";
    private const string LogsFolderName = "Logs";
    private const string Extension = "json";
    private const string Separator = "#";

    private readonly string _appDataFolderPath;

    private string GetLocalFolderPath()
    {
        var ltp = Path.Combine(_appDataFolderPath, FolderName);
        return ltp;
    }

    public string GetHistoricalDataFolderPath()
    {
        var hdp = Path.Combine(_appDataFolderPath, FolderName, HistoricalDataFolderName);
        return hdp;
    }

    public string GetLogFilePattern()
    {
        var ltp = Path.Combine(_appDataFolderPath, FolderName, LogsFolderName, "LTLog.txt");
        return ltp;
    }        
        
    public string GetTestLogFilePattern()
    {
        var ltp = Path.Combine(_appDataFolderPath, FolderName, LogsFolderName, "LTTestLog.txt");
        return ltp;
    }

    public void SaveLocalJson(string fileNamePart1, string fileNamePart2, string json)
    {
        var folder = GetLocalFolderPath();
        var jasonFileName = string.Concat(fileNamePart1, Separator, fileNamePart2);
        jasonFileName = Path.ChangeExtension(jasonFileName, Extension);
        var filePath = Path.Combine(folder, DataFolderName, jasonFileName);

        File.WriteAllText(filePath, json, Encoding.UTF8);
    }

    public void SaveLocalJson(string fileNamePart1, string fileNamePart2, string fileNamePart3, string json)
    {
        var folder = GetLocalFolderPath();
        var jasonFileName = string.Concat(fileNamePart1, Separator, fileNamePart2, Separator, fileNamePart3);
        jasonFileName = Path.ChangeExtension(jasonFileName, Extension);
        var filePath = Path.Combine(folder, DataFolderName, jasonFileName);

        File.WriteAllText(filePath, json, Encoding.UTF8);
    }
}