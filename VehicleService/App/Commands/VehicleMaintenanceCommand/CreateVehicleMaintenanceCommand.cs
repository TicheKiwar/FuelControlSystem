using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;

namespace VehicleService.App.Commands.VehicleMaintenanceCommand
{
    public class CreateVehicleMaintenanceCommand : IRequest<MessageResponse<VehicleMaintenance>>
    {
        public string VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool IsCompleted { get; set; }
        public string? ServiceProvider { get; set; }

        public CreateVehicleMaintenanceCommand(
            string vehicleId,
            DateTime startDate,
            DateTime? endDate,
            string description,
            decimal cost,
            bool isCompleted,
            string? serviceProvider)
        {
            VehicleId = vehicleId;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Cost = cost;
            IsCompleted = isCompleted;
            ServiceProvider = serviceProvider;
        }
    }
}
