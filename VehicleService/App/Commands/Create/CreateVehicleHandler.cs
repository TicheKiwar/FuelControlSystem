using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.App.Commands.Create
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, MessageResponse<Vehicle>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<MessageResponse<Vehicle>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = new Vehicle
                {
                    PlateNumber = request.PlateNumber,
                    Type = request.Type,
                    FuelType = request.FuelType,
                    FuelEfficiency = request.FuelEfficiency,
                    Brand = request.Brand,
                    Model = request.Model,
                    AcquisitionDate = request.AcquisitionDate,
                    AverageFuelEfficiency = request.AverageFuelEfficiency,
                    AverageSpeedKmPerHour = request.AverageSpeedKmPerHour,
                    PurchasePrice = request.PurchasePrice,
                    DocumentNumber = request.DocumentNumber,
                    IsUnderMaintenance = request.IsUnderMaintenance,
                };

                await _vehicleRepository.AddAsync(vehicle);

                return new MessageResponse<Vehicle>(vehicle, "Vehículo creado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<Vehicle>($"Error al crear vehículo: {ex.Message}");
            }
        }
    }
}
