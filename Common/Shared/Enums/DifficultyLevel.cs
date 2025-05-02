using System.Text.Json.Serialization;

namespace Common.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}
