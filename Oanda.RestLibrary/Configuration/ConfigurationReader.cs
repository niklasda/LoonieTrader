using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Oanda.RestLibrary.Configuration
{
    public class ConfigurationReader
    {
        private const string FileName = "Configuration.yaml";

        public Settings ReadConfiguration()
        {
            var fileContent = File.ReadAllText(FileName);
            var input = new StringReader(fileContent);

            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());

            var config = deserializer.Deserialize<Settings>(input);
            return config;
        }

        public Settings ReadConfigurationFrom(string directoryName)
        {
            var filePath = Path.Combine(directoryName, FileName);
            var fileContent = File.ReadAllText(filePath);
            var input = new StringReader(fileContent);

            var deserializer = new Deserializer(namingConvention: new PascalCaseNamingConvention());

            var config = deserializer.Deserialize<Settings>(input);
            return config;
        }
    }
}