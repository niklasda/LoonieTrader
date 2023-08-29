using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    [UsedImplicitly]
    public class OrdersRequester : RequesterBase, IOrdersRequester
    {
        public OrdersRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
            : base(settings, fileReaderWriter, logger)
        {
        }

        public OrdersResponse GetOrders(string accountId)
        {
            string urlOrders = base.GetRestUrl("accounts/{0}/orders"); // ?instrument=EUR_USD   ?state=PENDING    ?ids=177

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOrders, accountId);
                base.SaveLocalJson("orders", accountId, responseString);
            //    using (var input = new StringReader(responseString))
              //  {
                    var aor = JsonSerializer.Deserialize<OrdersResponse>(responseString);
                    return aor;
                //}
            }
        }

        public OrdersPendingResponse GetPendingOrders(string accountId)
        {
            string urlPendingOrders = base.GetRestUrl("accounts/{0}/pendingOrders/");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlPendingOrders, accountId);
                base.SaveLocalJson("ordersPending", accountId, responseString);
            //    using (var input = new StringReader(responseString))
              //  {
                    var aor = JsonSerializer.Deserialize<OrdersPendingResponse>(responseString);
                    return aor;
                //}
            }
        }

        public OrderDetailsResponse GetOrderDetails(string accountId, string orderId)
        {
            string urlOrderDetails = base.GetRestUrl("accounts/{0}/orders/{1}");

            using (var wc = GetAuthenticatedWebClient())
            {
                var responseString = GetData(wc, urlOrderDetails, accountId, orderId);
                base.SaveLocalJson("orderDetails", accountId, orderId, responseString);
              //  using (var input = new StringReader(responseString))
                //{
                    var aor = JsonSerializer.Deserialize<OrderDetailsResponse>(responseString);
                    return aor;
                //}
            }
        }

        public OrderCreateResponse PostCreateOrder(string accountId, OrderCreateResponse.OrderDefinition order)
        {
            string urlCreateOrder = base.GetRestUrl("accounts/{0}/orders/");

            using (var wc = GetAuthenticatedWebClient())
            {
                var orderJson = JsonSerializer.Serialize(order, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                base.Logger.Debug(orderJson.PrettyPrintJson());

                string responseString = PostData(wc, orderJson, urlCreateOrder, accountId);

                base.Logger.Debug(responseString.PrettyPrintJson());

      //          using (var input = new StringReader(responseString))
        //        {
                    var aor = JsonSerializer.Deserialize<OrderCreateResponse>(responseString);
                    return aor;
          //      }
            }
        }

        public OrderCreateResponse PutCancelOrder(string accountId, string orderId)
        {
            string urlCancelOrder = base.GetRestUrl("accounts/{0}/orders/{1}/cancel");
            using (var wc = GetAuthenticatedWebClient())
            {
                //var orderJson = JSON.Serialize(order, new Options(excludeNulls: true));
                //base.Logger.Debug(orderJson.PrettyPrintJson());

                string responseString = PutData(wc, null, urlCancelOrder, accountId, orderId);

                base.Logger.Debug(responseString.PrettyPrintJson());

            //    using (var input = new StringReader(responseString))
              //  {
                    var aor = JsonSerializer.Deserialize<OrderCreateResponse>(responseString);
                    return aor;
                //}
            }
        }
    }
}