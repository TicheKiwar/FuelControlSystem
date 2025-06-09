namespace FuelService.Domain.Entities;

public class CalculationResult
{
    public double EstimatedPrice { get; set; }
    public double EstimatedDurationMinutes { get; set; }
    public double EstimatedFuelLiters { get; set; }
}