using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;

namespace RouteService.App.Commands.Trips
{
    public class UpdateTripCommand : IRequest<MessageResponse<Trip>>
    {
        public string Id { get; set; }
        public string DriverId { get; set; }
        public string VehicleId { get; set; }
        public string RouteId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? ActualFuelConsumed { get; set; }
        public decimal? TotalCost { get; set; }
        public TripStatus Status { get; set; }
        public bool IsEmergency { get; set; }

        public UpdateTripCommand(
            string id,
            string driverId,
            string vehicleId,
            string routeId,
            DateTime? startTime,
            DateTime? endTime,
            double? actualFuelConsumed,
            decimal? totalCost,
            TripStatus status,
            bool isEmergency)
        {
            Id = id;
            DriverId = driverId;
            VehicleId = vehicleId;
            RouteId = routeId;
            StartTime = startTime;
            EndTime = endTime;
            ActualFuelConsumed = actualFuelConsumed;
            TotalCost = totalCost;
            Status = status;
            IsEmergency = isEmergency;
        }
    }

}
