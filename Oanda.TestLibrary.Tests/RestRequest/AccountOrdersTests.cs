using NUnit.Framework;
using Oanda.RestLibrary.Requester;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountOrdersTests
    {
        [Test]
        public void TestGetAccountOrders()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetOrders());
        }

        [Test]
        public void TestGetAccountPendingOrders()
        {
            var ar = new OandaRequester();
            Assert.NotNull(ar.GetPendingOrders());
        }
    }
}
