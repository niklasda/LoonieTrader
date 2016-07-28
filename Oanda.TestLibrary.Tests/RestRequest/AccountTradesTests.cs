using System;
using NUnit.Framework;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using Oanda.TestApp.Locator;

namespace Oanda.TestLibrary.Tests.RestRequest
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
