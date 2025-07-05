
using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

/// <summary>
/// Serializable class for calculation result
/// </summary>
[Serializable]
public class CalculationResult
{
    [JsonPropertyName("estimatedPrice")]
    public double EstimatedPrice { get; set; }
    [JsonPropertyName("estimatedDurationMinutes")]
    public double EstimatedDurationMinutes { get; set; }
    [JsonPropertyName("estimatedFuelLiters")]
    public double EstimatedFuelLiters { get; set; }
}