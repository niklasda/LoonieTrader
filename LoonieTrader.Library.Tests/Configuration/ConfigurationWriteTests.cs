//using System.Collections.Specialized;
//using LoonieTrader.Library.Constants;
//using LoonieTrader.Library.Interfaces;
//using LoonieTrader.Library.Models;
//using LoonieTrader.Library.Services;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace LoonieTrader.Library.Tests.Configuration;

//[TestClass, TestCategory("Integration")]
//public class ConfigurationWriteTests
//{
//    [TestMethod]
//    [Ignore("This overwrites the config")]
//    public void YamlDotNetWriteReadTest()
//    {
//        ISettings settings = new Settings();
//        settings.SelectedEnvironmentKey = Environments.Practice.Key;
//        settings.EnvironmentSettings = new []
//        {
//            new EnvironmentSettings()
//            {
//                ApiKey = "NA",
//                DefaultAccountId = "NA",
//                EnvironmentKey = Environments.Practice.Key,
//                FavoriteInstruments = new StringCollection { "EURUSD","USDSEK" }
//            },
//            new EnvironmentSettings()
//            {
//                ApiKey = "APIKEY-2",
//                DefaultAccountId = "Account-2",
//                EnvironmentKey = Environments.Live.Key,
//                FavoriteInstruments = new StringCollection { "EURUSD", "USDSEK" }
//            }
//        };

//        var frw = new FileReaderWriterService();
//        frw.SaveConfiguration(settings);



//        var s = frw.LoadConfiguration();
//        Assert.IsNotNull(s);

//    }
//}