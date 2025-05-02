using System.Text.Json.Serialization;

namespace Common.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TripStatus
    {
        Scheduled,
        InProgress,
        Completed,
        Cancelled,
        Delayed
    }
}
