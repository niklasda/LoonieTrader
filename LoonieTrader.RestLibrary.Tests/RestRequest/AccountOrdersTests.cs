using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequest
{
    public class AccountOrdersTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _or = container.GetInstance<IOrdersRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IOrdersRequester _or;
        private ISettings _s;

        [Test]
        public void TestGetAccountOrders()
        {
            Assert.NotNull(_or.GetOrders(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountPendingOrders()
        {
            Assert.NotNull(_or.GetPendingOrders(_s.DefaultAccountId));
        }
    }
}
