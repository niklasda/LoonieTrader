using NUnit.Framework;
using Oanda.RestLibrary.Interfaces;
using Oanda.RestLibrary.Requester;
using Oanda.TestApp.Locator;

namespace Oanda.TestLibrary.Tests.RestRequest
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
