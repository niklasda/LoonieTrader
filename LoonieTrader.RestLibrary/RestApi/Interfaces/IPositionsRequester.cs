using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Interfaces
{
    public interface IPositionsRequester
    {
        PositionsResponse GetPositions(string accountId);
        PositionsOpenResponse GetOpenPositions(string accountId);
        PositionsInstrumentResponse GetInstrumentPositions(string accountId, string instrument);
        PositionsCloseResponse PutClosePosition(string accountId, string instrument);
    }
}