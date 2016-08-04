using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters.v3
{
    public class TradesRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _tr = container.GetInstance<ITradesRequester>();
            _c = container.GetInstance<ISettings>();
        }

        private ITradesRequester _tr;
        private ISettings _c;

        [Test]
        public void TestGetAccountTrades()
        {
            var resp = _tr.GetTrades(_c.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
