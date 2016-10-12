namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IHeartbeatStreamable
    {
        string type { get; set; } // to support HEARTBEATS in streaming
    }
}