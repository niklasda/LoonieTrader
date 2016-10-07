using System;
using Jil;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestFixture, Category("Integration")]
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

        [Test]
        public void TestGetPricesVerifyJson()
        {
            PricingRequester pr = (PricingRequester)_pr;

            PricesResponse presp1 = _pr.GetPrices(_s.DefaultAccountId, "EUR_USD");
            string resp2 = pr.GetPricesJson(_s.DefaultAccountId, "EUR_USD");

            string resp1 = JSON.Serialize(presp1);

            Console.WriteLine(resp1);
            Console.WriteLine(resp2);

            //Assert.AreEqual(resp1.Length, resp2.Length);
        }
    }
}
