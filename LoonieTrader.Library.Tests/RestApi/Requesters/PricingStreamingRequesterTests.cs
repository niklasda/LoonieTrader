using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class PricingStreamingRequesterTests
{
    [TestInitialize]
    public void Setup()
    {
        var container = TestServiceLocator.Initialize();
        _txr = container.GetInstance<IPricingStreamingRequester>();
        _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
    }

    private IPricingStreamingRequester _txr;
    private IEnvironmentSettings _s;

    [TestMethod]
    public void TestPricingStream()
    {
        ObservableStream<PricesResponse.Price> pss = _txr.GetPriceStream(_s.DefaultAccountId, "EUR_USD");
        pss.NewValue += Pss_NewPrice;
        //            var l1 = pss.Subscribe(x => Console.WriteLine("Price1: {0}", x));

        Task.Delay(10000).Wait();
        Console.WriteLine("Done 10s");

        pss.NewValue -= Pss_NewPrice;
        //          l1.Dispose();

        // var price = JSON.Deserialize<PricesResponse.Price>(l1);
        // Assert.NotNull(price);
    }

    private void Pss_NewPrice(object sender, EventArgs e)
    {
        Console.WriteLine("Price1: ");
    }

    /*
    {"asks":[{"liquidity":10000000,"price":"1.11894"},{"liquidity":10000000,"price":"1.11896"}],"bids":[{"liquidity":10000000,"price":"1.11878"},{"liquidity":10000000,"price":"1.11876"}],"closeoutAsk":"1.11898","closeoutBid":"1.11874","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:14.269075446Z"}
    {"asks":[{"liquidity":10000000,"price":"1.11890"},{"liquidity":10000000,"price":"1.11892"}],"bids":[{"liquidity":10000000,"price":"1.11877"},{"liquidity":10000000,"price":"1.11875"}],"closeoutAsk":"1.11894","closeoutBid":"1.11873","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:17.759162246Z"}
    {"asks":[{"liquidity":10000000,"price":"1.11886"},{"liquidity":10000000,"price":"1.11888"}],"bids":[{"liquidity":10000000,"price":"1.11874"},{"liquidity":10000000,"price":"1.11872"}],"closeoutAsk":"1.11890","closeoutBid":"1.11870","instrument":"EUR_USD","status":"tradeable","time":"2016-10-07T13:58:20.322495500Z"}
    */
}