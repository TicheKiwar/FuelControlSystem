// Application/Events/TripAuditEvent.cs

using FuelService.Domain.Entities;

namespace FuelService.App.Events;
public class TripAuditEvent
{
    public string Action { get; set; }
    public TripAudit Trip { get; set; }
    public DriverInput Driver { get; set; }
    public VehicleInput Vehicle { get; set; }
    public RouteInput Route { get; set; }
}
    