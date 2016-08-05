using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters.v3
{
    public class TransactionsRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
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
    }
}
