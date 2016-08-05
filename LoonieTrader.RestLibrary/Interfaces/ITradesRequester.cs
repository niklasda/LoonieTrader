using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITradesRequester
    {
        AccountTradesResponse GetTrades(string accountId);
        AccountTradesResponse GetOpenTrades(string accountId);
        AccountTradesResponse GetTradeDetails(string accountId, string tradeId);

    }
}