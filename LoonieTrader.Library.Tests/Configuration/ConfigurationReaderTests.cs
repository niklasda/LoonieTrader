using NUnit.Framework;
using LoonieTrader.Library.Services;

namespace LoonieTrader.Library.Tests.Configuration
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