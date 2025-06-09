namespace FuelService.Domain.Entities;

public class TripAudit
{
    public string Id { get; set; } // Mongo _id
    public string TripId { get; set; }
    public DateTime CalculatedAt { get; set; }
    public CalculationInputs Inputs { get; set; }
    public CalculationResult CalculationResult { get; set; }
    public FinalResult FinalResult { get; set; }
}

