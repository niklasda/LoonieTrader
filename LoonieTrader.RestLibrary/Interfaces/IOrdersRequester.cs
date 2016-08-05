using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IOrdersRequester
    {
        AccountOrdersResponse GetOrders(string accountId);
        AccountPendingOrdersResponse GetPendingOrders(string accountId);
        AccountPendingOrdersResponse GetOrderDetails(string accountId, string orderId);
        AccountCreateOrdersResponse PostCreateOrder(string accountId, AccountCreateOrdersResponse.OrderDefinition order);
        AccountCreateOrdersResponse PutCancelOrder(string accountId, string orderId);
    }
}