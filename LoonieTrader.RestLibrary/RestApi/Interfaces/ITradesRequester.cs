using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface ITradesRequester
    {
        TradesResponse GetTrades(string accountId);
        TradesResponse GetOpenTrades(string accountId);
        TradeDetailsResponse GetTradeDetails(string accountId, string tradeId);

    }
}