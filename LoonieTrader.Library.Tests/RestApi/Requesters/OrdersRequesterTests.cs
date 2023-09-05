using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoonieTrader.Library.Tests.RestApi.Requesters;

[TestClass, TestCategory("Integration")]
public class OrdersRequesterTests : TestClassBase
{

    [TestMethod]
    public void TestGetAccountOrders()
    {
        OrdersResponse aor = OrdersReq.GetOrders(EnvSettings.DefaultAccountId);
        Assert.IsNotNull(aor);
        Console.WriteLine(aor);
    }

    [TestMethod]
    public void TestGetAccountPendingOrders()
    {
        OrdersPendingResponse apor = OrdersReq.GetPendingOrders(EnvSettings.DefaultAccountId);
        Assert.IsNotNull(apor);
        Console.WriteLine(apor);
    }

    [TestMethod]
    public void TestGetAccountOrderDetails()
    {
        OrderDetailsResponse aodr = OrdersReq.GetOrderDetails(EnvSettings.DefaultAccountId, "61");
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

        OrderCreateResponse aodr = OrdersReq.PostCreateOrder(EnvSettings.DefaultAccountId, od);
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


        OrderCreateResponse aodr = OrdersReq.PostCreateOrder(EnvSettings.DefaultAccountId, od);
        Assert.IsNotNull(aodr);

        Console.WriteLine(aodr);

    }
}