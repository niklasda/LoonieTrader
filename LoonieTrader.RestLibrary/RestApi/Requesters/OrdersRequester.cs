using System;
using System.IO;
using System.Net;
using System.Text;
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
                var responseBytes = wc.DownloadData(string.Format(urlOrders, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
                var responseBytes = wc.DownloadData(string.Format(urlPendingOrders, accountId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
                var responseBytes = wc.DownloadData(string.Format(urlOrderDetails, accountId, orderId));
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
                var orderBytes = Encoding.UTF8.GetBytes(orderJson);

                var responseBytes = wc.UploadData(string.Format(urlCreateOrder, accountId), "POST", orderBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
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
            throw new NotImplementedException();
        }
    }
}