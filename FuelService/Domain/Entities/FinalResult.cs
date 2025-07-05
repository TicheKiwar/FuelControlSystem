

using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

/// <summary>
/// Serializable class for final result
/// </summary>
[Serializable]
public class FinalResult
{
    [JsonPropertyName("actualFuelConsumed")]
    public double? ActualFuelConsumed { get; set; }
    [JsonPropertyName("actualDurationMinutes")]
    public double? ActualDurationMinutes { get; set; }
    [JsonPropertyName("actualPrice")]
    public double? ActualPrice { get; set; }
}