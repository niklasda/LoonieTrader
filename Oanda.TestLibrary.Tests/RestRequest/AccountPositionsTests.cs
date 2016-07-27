using System;
using NUnit.Framework;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using Oanda.TestApp.Locator;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountPositionsTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            var ar = container.GetInstance<IOandaRequester>();
            _cfg = container.GetInstance<ISettings>();

        }

        private ISettings _cfg;

        [Test]
        public void TestGetAccountPositions()
        {
            var ar = new OandaRequester();
            var resp = ar.GetPositions(_cfg.DefaultAccount);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountOpenPositions()
        {
            var ar = new OandaRequester();
            var resp = ar.GetOpenPositions(_cfg.DefaultAccount);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}