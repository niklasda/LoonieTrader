using NUnit.Framework;
using LoonieTrader.Library.Services;

namespace LoonieTrader.Library.Tests.Configuration
{
    [TestFixture, Category("Integration")]
    public class ConfigurationReadTests
    {
        [Test]
        public void YamlDotNetReadTest()
        {
            var frw = new FileReaderWriterService();
            var s = frw.LoadConfiguration();
            Assert.NotNull(s);
        }
    }
}