
using System;
using System.Text.Json.Serialization;
namespace FuelService.Domain.Entities;

/// <summary>
/// Serializable class for trip audit
/// </summary>
[Serializable]
public class TripAudit
{
    [JsonPropertyName("id")]
    public string? Id { get; set; } // Mongo _id
    [JsonPropertyName("tripId")]
    public string? TripId { get; set; }
    [JsonPropertyName("calculatedAt")]
    public DateTime CalculatedAt { get; set; }
    // Datos del viaje
    [JsonPropertyName("startTime")]
    public required DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; }
    [JsonPropertyName("inputs")]
    public CalculationInputs? Inputs { get; set; }
    [JsonPropertyName("calculationResult")]
    public CalculationResult? CalculationResult { get; set; }
    [JsonPropertyName("finalResult")]
    public FinalResult? FinalResult { get; set; }
}

