using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ITradesRequester
    {
        TradesResponse GetTrades(string accountId);
        TradesResponse GetOpenTrades(string accountId);
        TradeDetailsResponse GetTradeDetails(string accountId, string tradeId);

    }
}