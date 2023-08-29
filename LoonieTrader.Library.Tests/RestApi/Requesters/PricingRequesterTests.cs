using System;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class PricingRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _pr = container.GetInstance<IPricingRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private IPricingRequester _pr;
        private IEnvironmentSettings _s;

        [TestMethod]
        public void TestGetPrices()
        {
            var resp = _pr.GetPrices(_s.DefaultAccountId, "EUR_USD");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
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
