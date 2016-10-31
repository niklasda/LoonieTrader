using System.Collections.Specialized;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using NUnit.Framework;
using LoonieTrader.Library.Services;

namespace LoonieTrader.Library.Tests.Configuration
{
    [TestFixture, Category("Integration")]
    public class ConfigurationWriteTests
    {
        [Test]
        public void YamlDotNetWriteReadTest()
        {
            ISettings settings = new Settings();
            settings.SelectedEnvironmentKey = Environments.Practice.Key;
            settings.EnvironmentSettings = new []
            {
                new EnvironmentSettings()
                {
                    ApiKey = "APIKEY-1",
                    DefaultAccountId = "Account-1",
                    EnvironmentKey = Environments.Practice.Key,
                    FavouriteInstruments = new StringCollection {"INST1-1","INST1-2" }
                },
                new EnvironmentSettings()
                {
                    ApiKey = "APIKEY-2",
                    DefaultAccountId = "Account-2",
                    EnvironmentKey = Environments.Live.Key,
                    FavouriteInstruments = new StringCollection {"INST2-1","INST2-2" }
                }
            };

            var frw = new FileReaderWriterService();
            frw.SaveConfiguration(settings);



            var s = frw.LoadConfiguration();
            Assert.NotNull(s);

        }
    }
}