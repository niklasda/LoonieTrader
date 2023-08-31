using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.Tests.Locator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class OrdersRequesterTests
{
    [TestInitialize]
    public void Setup()
    {
        var container = TestServiceLocator.Initialize();
        _or = container.GetInstance<IOrdersRequester>();
        _s = container.GetInstance<ISettingsService>().CachedSettings.SelectedEnvironment;
    }

    private IOrdersRequester _or;
    private IEnvironmentSettings _s;

    [TestMethod]
    public void TestGetAccountOrders()
    {
        OrdersResponse aor = _or.GetOrders(_s.DefaultAccountId);
        Assert.IsNotNull(aor);
        Console.WriteLine(aor);
    }

    [TestMethod]
    public void TestGetAccountPendingOrders()
    {
        OrdersPendingResponse apor = _or.GetPendingOrders(_s.DefaultAccountId);
        Assert.IsNotNull(apor);
        Console.WriteLine(apor);
    }

    [TestMethod]
    public void TestGetAccountOrderDetails()
    {
        OrderDetailsResponse aodr = _or.GetOrderDetails(_s.DefaultAccountId, "61");
        Assert.IsNotNull(aodr);
        Console.WriteLine(aodr);
    }

    [TestMethod]
    public void TestPostCreateAccountMarketOrder()
    {
        var od = new OrderCreateResponse.OrderDefinition();
        od.order.units = "1000";
        od.order.instrument = "EUR_USD";
        od.order.timeInForce = TimeInForce.FOK.ToString();
        od.order.type = OrderTypes.MARKET.ToString();
        od.order.positionFill = OrderPositionFill.DEFAULT.ToString();

        OrderCreateResponse aodr = _or.PostCreateOrder(_s.DefaultAccountId, od);
        Assert.IsNotNull(aodr);

        Console.WriteLine(aodr);
    }

    [TestMethod]
    public void TestPostCreateAccountLimitOrder()
    {
        var od = new OrderCreateResponse.OrderDefinition();
        od.order.price = "1.200";
        od.order.units = "1000";
        od.order.instrument = "EUR_USD";
        od.order.timeInForce = TimeInForce.GTC.ToString();
        od.order.type = OrderTypes.LIMIT.ToString();
        od.order.positionFill = OrderPositionFill.DEFAULT.ToString();

        od.order.stopLossOnFill = new OrderCreateResponse.OrderDefinition.StopLossOnFill();
        od.order.stopLossOnFill.timeInForce = TimeInForce.GTC.ToString();
        od.order.stopLossOnFill.price = "1.000";


        OrderCreateResponse aodr = _or.PostCreateOrder(_s.DefaultAccountId, od);
        Assert.IsNotNull(aodr);

        Console.WriteLine(aodr);

    }
}