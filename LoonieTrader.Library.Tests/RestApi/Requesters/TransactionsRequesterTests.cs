using System;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestFixture, Category("Integration")]
    public class TransactionsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<ITransactionsRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private ITransactionsRequester _txr;
        private IEnvironmentSettings _s;

        [Test]
        public void TestGetAccountTransactionPages()
        {
            Assert.NotNull(_txr.GetTransactionPages(_s.DefaultAccountId));
        }

        [Test]
        public void TestGetAccountTransactions()
        {
            var resp = _txr.GetTransactions(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountTransactionDetails()
        {
            var resp = _txr.GetTransactionDetails(_s.DefaultAccountId, "3337");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }


    }
}
