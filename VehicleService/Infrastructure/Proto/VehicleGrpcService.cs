using Grpc.Core;
using MyApp.Grpc;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.Infrastructure.Proto
{
    public class VehicleGrpcService : MyApp.Grpc.VehicleService.VehicleServiceBase
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleGrpcService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public override async Task<VehicleResponse> GetVehicle(GetVehicleRequest request, ServerCallContext context)
        {

            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);

            if (vehicle == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Vhicle not found"));
            }

            var vehicleResponse = new VehicleResponse
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                AverageSpeedKmPerHour = vehicle.AverageSpeedKmPerHour,
                AverageFuelEfficiency = vehicle.AverageFuelEfficiency,
                IsUnderMaintenance = vehicle.IsUnderMaintenance
            };

            return vehicleResponse;
        }
    }
}
