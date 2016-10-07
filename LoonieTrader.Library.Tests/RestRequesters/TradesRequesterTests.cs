using System;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestRequesters
{
    [TestFixture, Category("Integration")]
    public class TradesRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
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
