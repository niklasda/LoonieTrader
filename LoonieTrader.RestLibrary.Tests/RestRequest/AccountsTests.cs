using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequest
{
    public class AccountsTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _ar = container.GetInstance<IAccountsRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IAccountsRequester _ar;
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
