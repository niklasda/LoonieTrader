using System.IO;
using System.Net;
using Jil;
using JsonPrettyPrinterPlus;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class OrdersRequester : RequesterBase, IOrdersRequester
    {
        public OrdersRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public OrdersResponse GetOrders(string accountId)
        {
            string urlOrders = base.GetRestUrl("accounts/{0}/orders"); // ?instrument=EUR_USD   ?state=PENDING    ?ids=177

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOrders, accountId);
                base.SaveLocalJson("orders", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrdersResponse>(input);
                    return aor;
                }
            }
        }

        public OrdersPendingResponse GetPendingOrders(string accountId)
        {
            string urlPendingOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlPendingOrders, accountId);
                base.SaveLocalJson("ordersPending", accountId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrdersPendingResponse>(input);
                    return aor;
                }
            }
        }

        public OrderDetailsResponse GetOrderDetails(string accountId, string orderId)
        {
            string urlOrderDetails = base.GetRestUrl("accounts/{0}/orders/{1}");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOrderDetails, accountId, orderId);
                base.SaveLocalJson("orderDetails", accountId, orderId, responseString);
                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrderDetailsResponse>(input);
                    return aor;
                }
            }
        }

        public OrderCreateResponse PostCreateOrder(string accountId, OrderCreateResponse.OrderDefinition order)
        {
            string urlCreateOrder = base.GetRestUrl("accounts/{0}/orders/");

            using (WebClient wc = GetAuthenticatedWebClient())
            {
                var orderJson = JSON.Serialize(order, new Options(excludeNulls: true));
                base.Logger.Debug(orderJson.PrettyPrintJson());

                string responseString = PostData(wc, orderJson, urlCreateOrder, accountId);

                base.Logger.Debug(responseString.PrettyPrintJson());

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrderCreateResponse>(input);
                    return aor;
                }
            }
        }

        public OrderCreateResponse PutCancelOrder(string accountId, string orderId)
        {
            string urlCancelOrder = base.GetRestUrl("accounts/{0}/orders/{1}/cancel");
            using (WebClient wc = GetAuthenticatedWebClient())
            {
                //var orderJson = JSON.Serialize(order, new Options(excludeNulls: true));
                //base.Logger.Debug(orderJson.PrettyPrintJson());

                string responseString = PutData(wc, null, urlCancelOrder, accountId, orderId);

                base.Logger.Debug(responseString.PrettyPrintJson());

                using (var input = new StringReader(responseString))
                {
                    var aor = JSON.Deserialize<OrderCreateResponse>(input);
                    return aor;
                }
            }
        }
    }
}