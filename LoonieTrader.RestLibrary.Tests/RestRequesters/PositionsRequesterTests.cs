using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class PositionsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _por = container.GetInstance<IPositionsRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IPositionsRequester _por;
        private ISettings _s;

        [Test]
        public void TestGetAccountPositions()
        {
            var resp = _por.GetPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountOpenPositions()
        {
            var resp = _por.GetOpenPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountInstrumentPositions()
        {
            var resp = _por.GetInstrumentPositions(_s.DefaultAccountId, "EUR_USD");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}