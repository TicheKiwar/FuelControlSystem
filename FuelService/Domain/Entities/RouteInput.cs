namespace FuelService.Domain.Entities;

public class RouteInput
{
    public string Id { get; set; }
    public string Name { get; set; }
    public LocationInput Origin { get; set; }
    public LocationInput Destination { get; set; }
    public double Distance { get; set; }
    public double EstimatedDuration { get; set; }
    public string DifficultyLevel { get; set; }
    public bool IsActive { get; set; }
}