using NUnit.Framework;
using Oanda.RestLibrary.Requester;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountsTests
    {
        [Test]
        public void TestGetAccounts()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetAccounts());
        }

        [Test]
        public void TestGetAccountSummary()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetAccountSummary());
        }
    }
}
