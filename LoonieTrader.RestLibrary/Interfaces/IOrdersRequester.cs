using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IOrdersRequester
    {
        AccountOrdersResponse GetOrders(string accountId);
        AccountPendingOrdersResponse GetPendingOrders(string accountId);
    }
}