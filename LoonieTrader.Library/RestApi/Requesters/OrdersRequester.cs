using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters;

[UsedImplicitly]
public class OrdersRequester : RequesterBase, IOrdersRequester
{
    public OrdersRequester(ISettingsService settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
        : base(settings, fileReaderWriter, logger)
    {
    }

    public OrdersResponse GetOrders(string accountId)
    {
        string urlOrders = GetRestUrl("accounts/{0}/orders"); // ?instrument=EUR_USD   ?state=PENDING    ?ids=177

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlOrders, accountId);
            SaveLocalJson("orders", accountId, responseString);
            //    using (var input = new StringReader(responseString))
            //  {
            var aor = JsonDeserialize<OrdersResponse>(responseString);
            return aor;
            //}
        }
    }

    public OrdersPendingResponse GetPendingOrders(string accountId)
    {
        string urlPendingOrders = GetRestUrl("accounts/{0}/pendingOrders/");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlPendingOrders, accountId);
            SaveLocalJson("ordersPending", accountId, responseString);
            //    using (var input = new StringReader(responseString))
            //  {
            var aor = JsonDeserialize<OrdersPendingResponse>(responseString);
            return aor;
            //}
        }
    }

    public OrderDetailsResponse GetOrderDetails(string accountId, string orderId)
    {
        string urlOrderDetails = GetRestUrl("accounts/{0}/orders/{1}");

        using (var wc = GetAuthenticatedWebClient())
        {
            var responseString = GetData(wc, urlOrderDetails, accountId, orderId);
            SaveLocalJson("orderDetails", accountId, orderId, responseString);
            //  using (var input = new StringReader(responseString))
            //{
            var aor = JsonDeserialize<OrderDetailsResponse>(responseString);
            return aor;
            //}
        }
    }

    public OrderCreateResponse PostCreateOrder(string accountId, OrderCreateResponse.OrderDefinition order)
    {
        string urlCreateOrder = GetRestUrl("accounts/{0}/orders/");

        using (var wc = GetAuthenticatedWebClient())
        {
            var orderJson = JsonSerializer.Serialize(order, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            Logger.Debug(orderJson.PrettyPrintJson());

            string responseString = PostData(wc, orderJson, urlCreateOrder, accountId);

            Logger.Debug(responseString.PrettyPrintJson());

            //          using (var input = new StringReader(responseString))
            //        {
            var aor = JsonDeserialize<OrderCreateResponse>(responseString);
            return aor;
            //      }
        }
    }

    public OrderCreateResponse PutCancelOrder(string accountId, string orderId)
    {
        string urlCancelOrder = GetRestUrl("accounts/{0}/orders/{1}/cancel");
        using (var wc = GetAuthenticatedWebClient())
        {
            //var orderJson = JSON.Serialize(order, new Options(excludeNulls: true));
            //base.Logger.Debug(orderJson.PrettyPrintJson());

            string responseString = PutData(wc, null, urlCancelOrder, accountId, orderId);

            Logger.Debug(responseString.PrettyPrintJson());

            //    using (var input = new StringReader(responseString))
            //  {
            var aor = JsonDeserialize<OrderCreateResponse>(responseString);
            return aor;
            //}
        }
    }
}