using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface IPositionsRequester
    {
        PositionsResponse GetPositions(string accountId);
        PositionsOpenResponse GetOpenPositions(string accountId);
        PositionsInstrumentResponse GetInstrumentPositions(string accountId, string instrument);
    }
}