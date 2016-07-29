using LoonieTrader.RestLibrary.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IPositionsRequester
    {
        AccountPositionsResponse GetPositions(string accountId);
        AccountOpenPositionsResponse GetOpenPositions(string accountId);
    }
}