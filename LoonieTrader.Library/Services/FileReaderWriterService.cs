using System;
using System.IO;
using System.Text;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LoonieTrader.Library.Services
{
    public class FileReaderWriterService : IFileReaderWriterService
    {
        public FileReaderWriterService()
        {
            _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dataPath = Path.Combine(_appDataFolderPath, FolderName, DataFolderName);
            string historicalDataPath = Path.Combine(_appDataFolderPath, FolderName, HistoricalDataFolderName);
            string indicatorPath = Path.Combine(_appDataFolderPath, FolderName,IndicatorsFolderName);

            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(historicalDataPath);
            Directory.CreateDirectory(indicatorPath);
            //File.Create(GetConfigFilePath());
        }

        private const string FileName = "Config.yaml";
        private const string FolderName = "LoonieTrader";
        private const string DataFolderName = "Data";
        private const string HistoricalDataFolderName = "HistoricalData";
        private const string IndicatorsFolderName = "Indicators";
        private const string LogsFolderName = "Logs";
        private const string Extension = "json";
        private const string Separator = "#";

        private readonly string _appDataFolderPath;

        public string GetLocalFolderPath()
        {
            var ltp = Path.Combine(_appDataFolderPath, FolderName);
            return ltp;
        }

        public string GetIndicatorFolderPath()
        {
            var ip = Path.Combine(_appDataFolderPath, FolderName, IndicatorsFolderName);
            return ip;
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
            using (FileStream fileStream = File.Open(GetConfigFilePath(), FileMode.OpenOrCreate))
            {
                using (StreamReader txtRd = new StreamReader(fileStream))
                {
                    string fileContent = txtRd.ReadToEnd();
                    var input = new StringReader(fileContent);

                    var desBuilder = new DeserializerBuilder();
                    var deserializer = desBuilder.WithNamingConvention(new PascalCaseNamingConvention()).IgnoreUnmatchedProperties().Build();

                    var config = deserializer.Deserialize<Settings>(input);

                    if (config?.EnvironmentSettings != null)
                    {
                        return config;
                    }

                    return Settings.Empty;
                }
            }
        }

        public void SaveConfiguration(ISettings settings)
        {
            using (FileStream fileStream = File.Open(GetConfigFilePath(), FileMode.OpenOrCreate))
            {
                using (StreamWriter txtWr = new StreamWriter(fileStream))
                {
                    var serBuilder = new SerializerBuilder();
                    var serializer = serBuilder.WithNamingConvention(new PascalCaseNamingConvention()).Build();

                    serializer.Serialize(txtWr, settings);
                }
            }
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