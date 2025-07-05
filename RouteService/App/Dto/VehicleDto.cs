namespace RouteService.App.Dto;
public class VehicleDto
{
    public string Id { get; set; }
    public string PlateNumber { get; set; }
    public double AverageSpeedKmPerHour { get; set; }
    public double AverageFuelEfficiency { get; set; }
    public bool IsUnderMaintenance { get; set; }
}