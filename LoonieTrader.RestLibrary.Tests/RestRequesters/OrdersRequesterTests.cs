using System;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Enums;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Responses;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters
{
    public class OrdersRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = TestServiceLocator.Initialize();
            _or = container.GetInstance<IOrdersRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IOrdersRequester _or;
        private ISettings _s;

        [Test]
        public void TestGetAccountOrders()
        {
            OrdersResponse aor = _or.GetOrders(_s.DefaultAccountId);
            Assert.NotNull(aor);
            Console.WriteLine(aor);
        }

        [Test]
        public void TestGetAccountPendingOrders()
        {
            OrdersPendingResponse apor = _or.GetPendingOrders(_s.DefaultAccountId);
            Assert.NotNull(apor);
            Console.WriteLine(apor);
        }

        [Test]
        public void TestGetAccountOrderDetails()
        {
            OrderDetailsResponse aodr = _or.GetOrderDetails(_s.DefaultAccountId, "61");
            Assert.NotNull(aodr);
            Console.WriteLine(aodr);
        }

        [Test]
        public void TestPostCreateAccountMarketOrder()
        {
            var od = new OrderCreateResponse.OrderDefinition();
            od.order.units = "1000";
            od.order.instrument = "EUR_USD";
            od.order.timeInForce = TimeInForce.FOK.ToString();
            od.order.type = OrderTypes.MARKET.ToString();
            od.order.positionFill = OrderPositionFill.DEFAULT.ToString();

            OrderCreateResponse aodr = _or.PostCreateOrder(_s.DefaultAccountId, od);
            Assert.NotNull(aodr);

            Console.WriteLine(aodr);
        }

        [Test]
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
            Assert.NotNull(aodr);

            Console.WriteLine(aodr);

        }
    }
}