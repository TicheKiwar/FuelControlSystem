using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.App.Commands.Update
{
    public class UpdateVehicleCommandHandler
        : IRequestHandler<UpdateVehicleCommand, MessageResponse<Vehicle>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<MessageResponse<Vehicle>> Handle(
            UpdateVehicleCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var existingVehicle = await _vehicleRepository.GetByIdAsync(request.Id);

                if (existingVehicle == null)
                {
                    return new MessageResponse<Vehicle>("Vehículo no encontrado");
                }

                existingVehicle.PlateNumber = request.PlateNumber;
                existingVehicle.Type = request.Type;
                existingVehicle.FuelType = request.FuelType;
                existingVehicle.FuelEfficiency = request.FuelEfficiency;
                existingVehicle.AverageFuelEfficiency = request.AverageFuelEfficiency;
                existingVehicle.AverageSpeedKmPerHour = request.AverageSpeedKmPerHour;
                existingVehicle.Brand = request.Brand;
                existingVehicle.Model = request.Model;

                await _vehicleRepository.UpdateAsync(existingVehicle);

                return new MessageResponse<Vehicle>(existingVehicle, "Vehículo actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<Vehicle>($"Error al actualizar: {ex.Message}");
            }
        }
    }
}
