using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.App.Commands.VehicleMaintenanceCommand.Update
{
    public class UpdateVehicleMaintenanceCommandHandler : IRequestHandler<UpdateVehicleMaintenanceCommand, MessageResponse<VehicleMaintenance>>
    {
        private readonly IVehicleMaintenance _vehicleMaintenanceRepository;

        public UpdateVehicleMaintenanceCommandHandler(IVehicleMaintenance vehicleMaintenanceRepository)
        {
            _vehicleMaintenanceRepository = vehicleMaintenanceRepository;
        }

        public async Task<MessageResponse<VehicleMaintenance>> Handle(UpdateVehicleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingMaintenance = await _vehicleMaintenanceRepository.GetByIdAsync(request.Id);

                if (existingMaintenance == null)
                {
                    return new MessageResponse<VehicleMaintenance>("Mantenimiento no encontrado");
                }

                existingMaintenance.VehicleId = request.VehicleId;
                existingMaintenance.StartDate = request.StartDate;
                existingMaintenance.EndDate = request.EndDate;
                existingMaintenance.Description = request.Description;
                existingMaintenance.Cost = request.Cost;
                existingMaintenance.IsCompleted = request.IsCompleted;
                existingMaintenance.ServiceProvider = request.ServiceProvider;

                await _vehicleMaintenanceRepository.UpdateAsync(existingMaintenance);

                return new MessageResponse<VehicleMaintenance>(existingMaintenance, "Mantenimiento actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<VehicleMaintenance>($"Error al actualizar mantenimiento: {ex.Message}");
            }
        }
    }
}
