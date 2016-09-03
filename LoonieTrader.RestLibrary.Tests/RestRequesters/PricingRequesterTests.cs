using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class PricingRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _pr = container.GetInstance<IPricingRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IPricingRequester _pr;
        private ISettings _s;

        [Test]
        public void TestGetPrices()
        {
            var resp = _pr.GetPrices(_s.DefaultAccountId, "EUR_USD");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
