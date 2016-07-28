using NUnit.Framework;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using Oanda.TestApp.Locator;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountsTests
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
        public void TestGetAccounts()
        {
            Assert.NotNull(_ar.GetAccounts());
        }

        [Test]
        public void TestGetAccountSummary()
        {
            Assert.NotNull(_ar.GetAccountSummary(_s.DefaultAccountId));
        }
    }
}
