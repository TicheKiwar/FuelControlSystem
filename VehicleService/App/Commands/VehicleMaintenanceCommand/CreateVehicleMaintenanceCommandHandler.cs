using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.App.Commands.VehicleMaintenanceCommand
{
    public class CreateVehicleMaintenanceCommandHandler : IRequestHandler<CreateVehicleMaintenanceCommand, MessageResponse<VehicleMaintenance>>
    {
        private readonly IVehicleMaintenance _vehicleMaintenanceRepository;

        public CreateVehicleMaintenanceCommandHandler(IVehicleMaintenance vehicleMaintenanceRepository)
        {
            _vehicleMaintenanceRepository = vehicleMaintenanceRepository;
        }

        public async Task<MessageResponse<VehicleMaintenance>> Handle(CreateVehicleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var maintenance = new VehicleMaintenance
                {
                    VehicleId = request.VehicleId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Description = request.Description,
                    Cost = request.Cost,
                    IsCompleted = request.IsCompleted,
                    ServiceProvider = request.ServiceProvider
                };

                await _vehicleMaintenanceRepository.AddAsync(maintenance);

                return new MessageResponse<VehicleMaintenance>(maintenance, "Mantenimiento creado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<VehicleMaintenance>($"Error al crear mantenimiento: {ex.Message}");
            }
        }
    }
}
