namespace RouteService.App.Dto;
public class TripFilterDto
{
    public string? DriverId { get; set; }
    public string? VehicleId { get; set; }
    public string? RouteId { get; set; }
    public Common.Shared.Enums.TripStatus? Status { get; set; }
    public bool? IsEmergency { get; set; }
    public DateTime? StartTimeFrom { get; set; }
    public DateTime? StartTimeTo { get; set; }
}