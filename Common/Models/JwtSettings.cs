using System.Text.Json.Serialization;

namespace Common.Models
{
    public class JwtSettings
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; } 

        [JsonPropertyName("Issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("Audience")]
        public string Audience { get; set; }

        [JsonPropertyName("ExpiryInMinutes")]
        public int ExpiryInMinutes { get; set; }
    }
}
