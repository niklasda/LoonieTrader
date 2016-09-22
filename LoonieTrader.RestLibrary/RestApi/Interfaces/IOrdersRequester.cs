using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IOrdersRequester
    {
        OrdersResponse GetOrders(string accountId);
        OrdersPendingResponse GetPendingOrders(string accountId);
        OrderDetailsResponse GetOrderDetails(string accountId, string orderId);
        OrderCreateResponse PostCreateOrder(string accountId, OrderCreateResponse.OrderDefinition order);
        OrderCreateResponse PutCancelOrder(string accountId, string orderId);
    }
}