using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using RouteService.Domain.Entities;

namespace RouteService.App.Commands.Trips
{
    public class CreateTripCommand : IRequest<MessageResponse<Trip>>
    {
        public string DriverId { get; set; }
        public string VehicleId { get; set; }
        public string RouteId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double ExpectedFuelConsumed { get; set; }
        public decimal? TotalCost { get; set; }
        public TripStatus Status { get; set; }
        public bool IsEmergency { get; set; } = false;

        public CreateTripCommand(
            string driverId,
            string vehicleId,
            string routeId,
            DateTime startTime,
            DateTime? endTime,
            double expectedFuelConsumed,
            decimal? totalCost,
            TripStatus status,
            bool isEmergency)
        {
            DriverId = driverId;
            VehicleId = vehicleId;
            RouteId = routeId;
            StartTime = startTime;
            EndTime = endTime;
            ExpectedFuelConsumed = expectedFuelConsumed;
            TotalCost = totalCost;
            Status = status;
            IsEmergency = isEmergency;
        }
    }
}
