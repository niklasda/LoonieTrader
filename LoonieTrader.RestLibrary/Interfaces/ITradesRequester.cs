using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITradesRequester
    {
        AccountTradesResponse GetTrades(string accountId);

    }
}