
using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

/// <summary>
/// Serializable class for route input
/// </summary>
[Serializable]
public class RouteInput
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("origin")]
    public LocationInput? Origin { get; set; }
    [JsonPropertyName("destination")]
    public LocationInput? Destination { get; set; }
    [JsonPropertyName("distance")]
    public double Distance { get; set; }
    [JsonPropertyName("estimatedDuration")]
    public double EstimatedDuration { get; set; }
    [JsonPropertyName("difficultyLevel")]
    public string? DifficultyLevel { get; set; }
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
}