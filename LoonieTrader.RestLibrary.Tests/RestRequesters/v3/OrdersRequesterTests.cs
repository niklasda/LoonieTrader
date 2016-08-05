using System;
using System.Diagnostics;
using NUnit.Framework;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using LoonieTrader.RestLibrary.Tests.Locator;

namespace LoonieTrader.RestLibrary.Tests.RestRequesters.v3
{
    public class OrdersRequesterTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var container = ServiceLocator.Initialize();
            _or = container.GetInstance<IOrdersRequester>();
            _s = container.GetInstance<ISettings>();
        }

        private IOrdersRequester _or;
        private ISettings _s;

        [Test]
        public void TestGetAccountOrders()
        {
            AccountOrdersResponse aor = _or.GetOrders(_s.DefaultAccountId);
            Assert.NotNull(aor);
            foreach (AccountOrdersResponse.Order o in aor.orders)
            {
                Console.WriteLine(o.id);
            }
        }

        [Test]
        public void TestGetAccountPendingOrders()
        {
            AccountPendingOrdersResponse apor = _or.GetPendingOrders(_s.DefaultAccountId);
            Assert.NotNull(apor);
            foreach (AccountPendingOrdersResponse.PendingOrder o in apor.orders)
            {
                Console.WriteLine(o.id);
            }
        }

        [Test]
        public void TestGetAccountOrderDetails()
        {
            AccountPendingOrdersResponse aodr = _or.GetOrderDetails(_s.DefaultAccountId, "");
            Assert.NotNull(aodr);
        }

        [Test]
        public void TestGetCreateAccountMarketOrderDetails()
        {
            var od = new AccountCreateOrdersResponse.OrderDefinition();
            od.order.units = "1000";
            od.order.instrument = "EUR_USD";
            od.order.timeInForce = "FOK";
            od.order.type = "MARKET";
            od.order.positionFill = "DEFAULT";


            AccountCreateOrdersResponse aodr = _or.PostCreateOrder(_s.DefaultAccountId, od);
            Assert.NotNull(aodr);

            Console.WriteLine(aodr);
        }

        [Test]
        public void TestGetCreateAccountLimitOrderDetails()
        {
            var od = new AccountCreateOrdersResponse.OrderDefinition();
            od.order.price = "1.500";
            od.order.units = "1000";
            od.order.instrument = "EUR_USD";
            od.order.timeInForce = "GTC";
            od.order.type = "LIMIT";
            od.order.positionFill = "DEFAULT";

            od.order.stopLossOnFill.timeInForce = "GTC";
            od.order.stopLossOnFill.price = "1.450";


            AccountCreateOrdersResponse aodr = _or.PostCreateOrder(_s.DefaultAccountId, od);
            Assert.NotNull(aodr);

            Console.WriteLine(aodr);

        }
    }
}