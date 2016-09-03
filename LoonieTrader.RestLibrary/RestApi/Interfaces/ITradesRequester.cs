using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Interfaces
{
    public interface ITradesRequester
    {
        TradesResponse GetTrades(string accountId);
        TradesResponse GetOpenTrades(string accountId);
        TradeDetailsResponse GetTradeDetails(string accountId, string tradeId);

    }
}