using System.Text.Json.Serialization;

namespace LoonieTrader.Library.RestApi.Interfaces
{
    public interface IHeartbeatStreamable
    {
        [JsonPropertyName("type")]
        string EventType { get; set; } // to support HEARTBEATS in streaming
    }
}