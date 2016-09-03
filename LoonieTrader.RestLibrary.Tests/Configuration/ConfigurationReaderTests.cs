using NUnit.Framework;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Services;

namespace LoonieTrader.RestLibrary.Tests.Configuration
{
    public class ConfigurationReaderTests
    {
        [Test]
        public void YamlDotNetTest()
        {
            var cr = new FileReaderWriterService();
            var s = cr.LoadConfiguration();
            Assert.NotNull(s);
        }
    }
}