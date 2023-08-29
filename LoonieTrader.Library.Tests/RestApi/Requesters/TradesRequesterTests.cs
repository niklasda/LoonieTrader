using System;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class TradesRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _tr = container.GetInstance<ITradesRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private ITradesRequester _tr;
        private IEnvironmentSettings _s;

        [TestMethod]
        public void TestGetAccountTrades()
        {
            var resp = _tr.GetTrades(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetAccountOpenTrades()
        {
            var resp = _tr.GetOpenTrades(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetAccountTradeDetails()
        {
            var resp = _tr.GetTradeDetails(_s.DefaultAccountId, "37");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }
    }
}
