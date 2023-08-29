using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;

namespace LoonieTrader.Library.Tests.RestApi.Requesters
{
    [TestClass, TestCategory("Integration")]
    public class TransactionsStreamingRequesterTests
    {
        [TestInitialize]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<ITransactionsStreamingRequester>();
            _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
        }

        private ITransactionsStreamingRequester _txr;
        private IEnvironmentSettings _s;

        [TestMethod]
        public void TestTransactionStream()
        {
            ObservableStream<TransactionsResponse.Transaction> tss = _txr.GetTransactionStream(_s.DefaultAccountId);
            tss.NewValue += Tss_NewPrice;
            //IDisposable l1 = tss.Subscribe(x => Console.WriteLine("Tx1: {0}", x));

            Task.Delay(10000).Wait();
            Console.WriteLine("Done. 10s");

            tss.NewValue -= Tss_NewPrice;
           // l1.Dispose();

            //            var price = JSON.Deserialize<TransactionsResponse.Transaction>(l1);
            //          Assert.NotNull(price);
        }

        private void Tss_NewPrice(object sender, EventArgs e)
        {
            Console.WriteLine("Tx1: ");
        }

        /*
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:45.434095856Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:50.454075397Z","type":"HEARTBEAT"}
        {"lastTransactionID":"2871","time":"2016-10-07T14:05:55.551386493Z","type":"HEARTBEAT"}
        */
    }
}
