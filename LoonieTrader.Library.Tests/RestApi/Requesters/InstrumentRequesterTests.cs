using System;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class InstrumentRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _ir = container.GetInstance<IInstrumentRequester>();
        }

        private IInstrumentRequester _ir;

        [TestMethod]
        public void TestGetCandlesDefaults()
        {
            var resp = _ir.GetCandles("EUR_USD");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetCandlesError()
        {
            var resp = _ir.GetCandles("EUXYZSD");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetCandlesS5CountM5()
        {
            var resp = _ir.GetCandles("EUR_USD", CandlestickGranularity.S5, "M", 5);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetCandlesS5CountA5()
        {
            var resp = _ir.GetCandles("GBP_USD", CandlestickGranularity.S15, "A", 5);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetCandlesS5CountB5()
        {
            var resp = _ir.GetCandles("USD_SEK", CandlestickGranularity.S30, "B", 5);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetCandlesS5CountDate()
        {
            var resp = _ir.GetCandles("EUR_USD", DateTime.Now.AddMonths(-1));
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

    }
}
