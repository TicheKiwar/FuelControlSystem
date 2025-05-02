using System.Text.Json.Serialization;

namespace Common.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FuelType
    {
        Gasoline,     
        Diesel,       
        Electric,     
        Hybrid,       
        Ethanol
    }
}
