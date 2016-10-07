using System;
using System.Threading.Tasks;
using NUnit.Framework;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
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
            IObservable<string> pss = _txr.GetTransactionStream(_s.DefaultAccountId);

            IDisposable l1 = pss.Subscribe(x => Console.WriteLine("Tx1: {0}", x));

            Task.Delay(10000).Wait();
            Console.WriteLine("Done. 10s");

            l1.Dispose();

            //            var price = JSON.Deserialize<TransactionsResponse.Transaction>(l1);
            //          Assert.NotNull(price);
        }

        /*
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:45.434095856Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:50.454075397Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:55.551386493Z","type":"HEARTBEAT"}
        */
    }
}
