using System;
using System.Text.Json.Serialization;

[Serializable]
public class VehicleInput
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("plateNumber")]
    public string? PlateNumber { get; set; }
    [JsonPropertyName("fuelEfficiency")]
    public string? FuelEfficiency { get; set; }
    [JsonPropertyName("averageFuelEfficiency")]
    public string? AverageFuelEfficiency { get; set; }
    [JsonPropertyName("isUnderMaintenance")]
    public bool IsUnderMaintenance { get; set; }
}