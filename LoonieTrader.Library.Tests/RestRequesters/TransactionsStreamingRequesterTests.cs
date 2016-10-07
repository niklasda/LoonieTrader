using System;
using System.IO;
using Jil;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestRequesters
{
    [TestFixture, Category("Integration")]
    public class TransactionsStreamingRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<ITransactionsStreamingRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private ITransactionsStreamingRequester _txr;
        private ISettings _s;

        [Test]
        public void TestPricingStream()
        {
            StreamReader pss = _txr.GetTransactionStream(_s.DefaultAccountId);
            var l1 = pss.ReadLine();
            Console.WriteLine(l1);
            var l2 = pss.ReadLine();
            Console.WriteLine(l2);
            var l3 = pss.ReadLine();
            Console.WriteLine(l3);
            pss.Close();

            var price = JSON.Deserialize<TransactionsResponse.Transaction>(l1);
            Assert.NotNull(price);

        }
        /*
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:45.434095856Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:50.454075397Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:55.551386493Z","type":"HEARTBEAT"}
        */
    }
}
