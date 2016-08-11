using System;
using System.IO;
using System.Text;
using LoonieTrader.RestLibrary.Interfaces;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LoonieTrader.RestLibrary.Configuration
{
    public class FileReaderWriter : IFileReaderWriter
    {
        public FileReaderWriter()
        {
            _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dataPath = Path.Combine(_appDataFolderPath, FolderName, DataFolderName);
            string historicalDataPath = Path.Combine(_appDataFolderPath, FolderName, HistoricalDataFolderName);
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(historicalDataPath);
        }

        private const string FileName = "Config.yaml";
        private const string FolderName = "LoonieTrader";
        private const string DataFolderName = "Data";
        private const string HistoricalDataFolderName = "HistoricalData";
        private const string LogsFolderName = "Logs";
        private const string Extension = "json";
        private const string Separator = "#";

        private readonly string _appDataFolderPath;

        public string GetLocalFolderPath()
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

        private string GetConfigFilePath()
        {
            var up = GetLocalFolderPath();
            var ltp = Path.Combine(up, FileName);
            return ltp;
        }

        public ISettings LoadConfiguration()
        {
            var fileContent = File.ReadAllText(GetConfigFilePath());
            var input = new StringReader(fileContent);

            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());

            var config = deserializer.Deserialize<Settings>(input);
            return config;
        }

        public void SaveConfiguration(ISettings settings)
        {
        }

        public string LoadLocalJson(string fileNamePart1, string fileNamePart2)
        {
            var folder = GetLocalFolderPath();
            var jasonFileName = string.Concat(fileNamePart1, Separator, fileNamePart2);
            jasonFileName = Path.ChangeExtension(jasonFileName, Extension);
            var filePath = Path.Combine(folder, jasonFileName);

            var json = File.ReadAllText(filePath, Encoding.UTF8);
            return json;
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
}