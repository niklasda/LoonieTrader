using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IPositionsRequester
    {
        AccountPositionsResponse GetPositions(string accountId);
        AccountOpenPositionsResponse GetOpenPositions(string accountId);
        AccountInstrumentPositionResponse GetInstrumentPositions(string accountId, string instrument);
    }
}