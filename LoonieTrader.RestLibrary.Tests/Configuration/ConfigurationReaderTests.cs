using NUnit.Framework;
using LoonieTrader.RestLibrary.Configuration;

namespace LoonieTrader.RestLibrary.Tests.Configuration
{
    public class ConfigurationReaderTests
    {
        [Test]
        public void YamlDotNetTest()
        {
            var cr = new FileReaderWriter();
            var s = cr.LoadConfiguration();
            Assert.NotNull(s);
        }
    }
}