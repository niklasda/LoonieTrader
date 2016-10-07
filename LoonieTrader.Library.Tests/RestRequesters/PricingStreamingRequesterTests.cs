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
    public class PricingStreamingRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _txr = container.GetInstance<IPricingStreamingRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IPricingStreamingRequester _txr;
        private ISettings _s;

        [Test]
        public void TestPricingStream()
        {
            StreamReader pss = _txr.GetPriceStream(_s.DefaultAccountId, "EUR_USD");
            var l1 = pss.ReadLine();
            Console.WriteLine(l1);
            var l2 = pss.ReadLine();
            Console.WriteLine(l2);
            var l3 = pss.ReadLine();
            Console.WriteLine(l3);
            pss.Close();

            var price = JSON.Deserialize<PricesResponse.Price>(l1);
            Assert.NotNull(price);
        }

        /*
        {"asks":[{"liquidity":10000000,"price":"1.11894"},{"liquidity":10000000,"price":"1.11896"}],"bids":[{"liquidity":10000000,"price":"1.11878"},{"liquidity":10000000,"price":"1.11876"}],"closeoutAsk":"1.11898","closeoutBid":"1.11874","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:14.269075446Z"}
        {"asks":[{"liquidity":10000000,"price":"1.11890"},{"liquidity":10000000,"price":"1.11892"}],"bids":[{"liquidity":10000000,"price":"1.11877"},{"liquidity":10000000,"price":"1.11875"}],"closeoutAsk":"1.11894","closeoutBid":"1.11873","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:17.759162246Z"}
        {"asks":[{"liquidity":10000000,"price":"1.11886"},{"liquidity":10000000,"price":"1.11888"}],"bids":[{"liquidity":10000000,"price":"1.11874"},{"liquidity":10000000,"price":"1.11872"}],"closeoutAsk":"1.11890","closeoutBid":"1.11870","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:20.322495500Z"}
        */


    }
}
