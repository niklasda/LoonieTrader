using System;
using NUnit.Framework;
using Oanda.RestLibrary.Requester;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountTradesTests
    {
        [Test]
        public void TestGetAccountTrades()
        {
            var ar = new OandaRequester();
            var resp = ar.GetTrades();
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
        
    }
}
