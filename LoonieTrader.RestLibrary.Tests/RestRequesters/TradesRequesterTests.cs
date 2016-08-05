using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class TradesRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _tr = container.GetInstance<ITradesRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private ITradesRequester _tr;
        private ISettings _s;

        [Test]
        public void TestGetAccountTrades()
        {
            var resp = _tr.GetTrades(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountOpenTrades()
        {
            var resp = _tr.GetOpenTrades(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountTradeDetails()
        {
            var resp = _tr.GetTradeDetails(_s.DefaultAccountId, "37");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
