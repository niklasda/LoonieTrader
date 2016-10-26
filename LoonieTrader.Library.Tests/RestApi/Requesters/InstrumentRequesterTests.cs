using System;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestFixture, Category("Integration")]
    public class InstrumentRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _ir = container.GetInstance<IInstrumentRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private IInstrumentRequester _ir;
        private IEnvironmentSettings2 _s;

        [Test]
        public void TestGetCandlesDefaults()
        {
            var resp = _ir.GetCandles("EUR_USD");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetCandlesError()
        {
            var resp = _ir.GetCandles("EUXYZSD");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetCandlesS5CountM5()
        {
            var resp = _ir.GetCandles("EUR_USD", CandlestickGranularity.S5, "M", 5);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetCandlesS5CountA5()
        {
            var resp = _ir.GetCandles("GBP_USD", CandlestickGranularity.S15, "A", 5);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetCandlesS5CountB5()
        {
            var resp = _ir.GetCandles("USD_SEK", CandlestickGranularity.S30, "B", 5);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetCandlesS5CountDate()
        {
            var resp = _ir.GetCandles("EUR_USD", DateTime.Now.AddMonths(-1));
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

    }
}
