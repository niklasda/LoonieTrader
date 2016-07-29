using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Configuration;

namespace LoonieTrader.RestLibrary.Tests.Configuration
{
    public class ConfigurationReaderTests
    {
        [Test]
        public void YamlDotNetTest()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var cr = new ConfigurationReader();
            var s = cr.ReadConfigurationFrom(directoryName);
            Assert.NotNull(s);
        }
    }
}