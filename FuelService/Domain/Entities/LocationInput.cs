
using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

    /// <summary>
    /// Serializable class for location input
    /// </summary>
    [Serializable]
    public class LocationInput
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
