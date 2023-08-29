using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class PositionsRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _por = container.GetInstance<IPositionsRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private IPositionsRequester _por;
        private IEnvironmentSettings _s;

        [TestMethod]
        public void TestGetAccountPositions()
        {
            var resp = _por.GetPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetAccountOpenPositions()
        {
            var resp = _por.GetOpenPositions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestGetAccountInstrumentPositions()
        {
            var resp = _por.GetInstrumentPositions(_s.DefaultAccountId, "EUR_USD");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void TestCloseAccountInstrumentPositions()
        {
            var resp = _por.PutClosePosition(_s.DefaultAccountId, "EUR_USD");
            Console.WriteLine(resp);
            Assert.IsNotNull(resp);
        }
    }
}