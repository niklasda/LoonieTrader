using NUnit.Framework;
using Oanda.RestLibrary.Requester;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountTransactionsTests
    {
        [Test]
        public void TestGetAccountTransactionPages()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetTransactionPages());
        }

        [Test]
        public void TestGetAccountTransactions()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetTransactions());
        }

    }
}
