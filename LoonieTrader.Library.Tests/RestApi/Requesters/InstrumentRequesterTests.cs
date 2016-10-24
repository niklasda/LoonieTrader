using System;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
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
            _s = container.GetInstance<ISettings>();
        }

        private IInstrumentRequester _ir;
        private ISettings _s;

        [Test]
        public void TestGetCandles()
        {
            var resp = _ir.GetCandles("EUR_USD");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

    }
}
