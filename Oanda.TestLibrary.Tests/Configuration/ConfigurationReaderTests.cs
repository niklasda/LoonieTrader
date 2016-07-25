using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Oanda.RestLibrary.Configuration;

namespace Oanda.TestLibrary.Tests.Configuration
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