using System.Text.Json.Serialization;

namespace Common.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LicenseType
    {
        B, // Vehículos livianos
        E, // Vehículos pesados
        G  // Maquinaria pesada y ligera
    }
}
