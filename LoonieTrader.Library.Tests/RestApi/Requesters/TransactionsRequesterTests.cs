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
            _s = container.GetInstance<ISettings>();
        }

        private ITransactionsRequester _txr;
        private ISettings _s;

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
            var resp = _txr.GetTransactionDetails(_s.DefaultAccountId, "37");
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test, Explicit("might not be available yet")]
        public void TestGetAccountTransactionStream()
        {
            var resp = _txr.GetTransactionStream(_s.DefaultAccountId);
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
