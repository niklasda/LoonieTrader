using System.Text.Json;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class PricingRequesterTests : TestClassBase
{
    //[TestInitialize]
    //public void Setup()
    //{
    //    var container = TestServiceLocator.Initialize();
    //    PricingReq = container.GetInstance<IPricingRequester>();
    //    EnvSettings = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
    //}

    //private IPricingRequester PricingReq;
    //private IEnvironmentSettings EnvSettings;

    [TestMethod]
    public void TestGetPrices()
    {
        var resp = PricingReq.GetPrices(EnvSettings.DefaultAccountId, "EUR_USD");
        Console.WriteLine(resp);
        Assert.IsNotNull(resp);
    }

    [TestMethod]
    public void TestGetPricesVerifyJson()
    {
        PricingRequester pr = (PricingRequester)PricingReq;

        PricesResponse presp1 = PricingReq.GetPrices(EnvSettings.DefaultAccountId, "EUR_USD");
        string resp2 = pr.GetPricesJson(EnvSettings.DefaultAccountId, "EUR_USD");

        string resp1 = JsonSerializer.Serialize(presp1);

        Console.WriteLine(resp1);
        Console.WriteLine(resp2);

        //Assert.AreEqual(resp1.Length, resp2.Length);
    }
}