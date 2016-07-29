using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequest
{
    public class AccountTransactionsTests
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
        public void TestGetAccountTransactionPages()
        {
            Assert.NotNull(_ar.GetTransactionPages(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountTransactions()
        {
            Assert.NotNull(_ar.GetTransactions(_s.DefaultAccountId));
        }

    }
}
