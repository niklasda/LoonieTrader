using LoonieTrader.Library.Services;

namespace LoonieTrader.Library.Tests.Configuration
{
    [TestClass, TestCategory("Integration")]
    public class ConfigurationReadTests
    {
        [TestMethod]
        public void YamlDotNetReadTest()
        {
            var frw = new FileReaderWriterService();
            var s = frw.LoadConfiguration();
            Assert.IsNotNull(s);
        }
    }
}