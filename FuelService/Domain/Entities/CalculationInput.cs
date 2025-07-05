
using System;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
namespace FuelService.Domain.Entities;


/// <summary>
/// Serializable class for calculation inputs
/// </summary>
 [Serializable]
 public class CalculationInputs
{
    [JsonPropertyName("vehicle")]
    public VehicleInput? Vehicle { get; set; }
    [JsonPropertyName("driver")]
    public DriverInput? Driver { get; set; }
    [JsonPropertyName("route")]
    public RouteInput? Route { get; set; }
}