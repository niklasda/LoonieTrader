using System;
using NUnit.Framework;
using Oanda.RestLibrary.Requester;

namespace Oanda.TestLibrary.Tests.RestRequest
{
    public class AccountPositionsTests
    {
        [Test]
        public void TestGetAccountPositions()
        {
            var ar = new OandaRequester();
            var resp = ar.GetPositions();
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }

        [Test]
        public void TestGetAccountOpenPositions()
        {
            var ar = new OandaRequester();
            var resp = ar.GetOpenPositions();
            Console.WriteLine(resp);
            Assert.NotNull(resp);
        }
    }
}
