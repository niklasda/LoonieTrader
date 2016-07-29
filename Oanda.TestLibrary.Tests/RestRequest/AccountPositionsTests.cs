using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequest
{
    public class AccountPositionsTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _ar = container.GetInstance<IOandaRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IOandaRequester _ar;
        private ISettings _s;

        [Test]
        public void TestGetAccountPositions()
        {
            var resp = _ar.GetPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountOpenPositions()
        {
            var resp = _ar.GetOpenPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}