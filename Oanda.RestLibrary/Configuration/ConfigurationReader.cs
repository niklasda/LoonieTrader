using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LoonieTrader.RestLibrary.Configuration
{
    public class ConfigurationReader
    {
        private string GetConfigFileName()
        {
            const string fileName = "Config.yaml";
            var up = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var ltp = Path.Combine(up, "LoonieTrader", fileName);
            return ltp;
        }

        public Settings ReadConfiguration()
        {
            var fileContent = File.ReadAllText(GetConfigFileName());
            var input = new StringReader(fileContent);

            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());

            var config = deserializer.Deserialize<Settings>(input);
            return config;
        }

        public Settings ReadConfigurationFrom(string directoryName)
        {
            var filePath = Path.Combine(directoryName, GetConfigFileName());
            var fileContent = File.ReadAllText(filePath);
            var input = new StringReader(fileContent);

            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());

            var config = deserializer.Deserialize<Settings>(input);
            return config;
        }
    }
}