
using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

/// <summary>
/// Serializable class for driver input
/// </summary>
[Serializable]
public class DriverInput
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }
    [JsonPropertyName("hourlyRate")]
    public string? HourlyRate { get; set; }
}
