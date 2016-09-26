using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IPositionsRequester
    {
        PositionsResponse GetPositions(string accountId);
        PositionsOpenResponse GetOpenPositions(string accountId);
        PositionsInstrumentResponse GetInstrumentPositions(string accountId, string instrument);
        PositionsCloseResponse PutClosePosition(string accountId, string instrument);
    }
}