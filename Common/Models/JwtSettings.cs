using System.Text.Json.Serialization;

namespace Common.Models
{
    public class JwtSettings
    {
        [JsonPropertyName("Key")]
        public string Secret { get; set; } 

        [JsonPropertyName("Issuer")]
        public string TokenIssuer { get; set; }

        [JsonPropertyName("Audience")]
        public string TokenAudience { get; set; }

        [JsonPropertyName("ExpiryInMinutes")]
        public int ExpirationMinutes { get; set; }
    }
}
