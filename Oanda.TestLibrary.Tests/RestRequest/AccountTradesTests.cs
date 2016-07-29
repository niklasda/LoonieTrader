using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequest
{
    public class AccountTradesTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _ar = container.GetInstance<IOandaRequester>();
            _c = container.GetInstance<ISettings>();
        }

        private IOandaRequester _ar;
        private ISettings _c;

        [Test]
        public void TestGetAccountTrades()
        {
            var resp = _ar.GetTrades(_c.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
