using System.Text.Json.Serialization;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IHeartbeatStreamable
    {
        string EventType { get; set; } // to support HEARTBEATS in streaming
    }
}