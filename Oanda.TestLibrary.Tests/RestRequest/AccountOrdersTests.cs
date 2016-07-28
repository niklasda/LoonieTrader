using NUnit.Framework;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using Oanda.TestApp.Locator;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountOrdersTests
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
        public void TestGetAccountOrders()
        {
            Assert.NotNull(_ar.GetOrders(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountPendingOrders()
        {
            Assert.NotNull(_ar.GetPendingOrders(_s.DefaultAccountId));
        }
    }
}
