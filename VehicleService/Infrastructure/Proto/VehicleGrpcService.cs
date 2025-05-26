using Grpc.Core;
using MyApp.Grpc;
using VehicleService.Domain.Interfaces.Repositories;

namespace VehicleService.Infrastructure.Proto
{
    public class VehicleGrpcService : MyApp.Grpc.VehicleService.VehicleServiceBase
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleGrpcService(IVehicleRepository driverRepository)
        {
            _vehicleRepository = driverRepository;
        }
        public override async Task<VehicleResponse> GetVehicle(GetVehicleRequest request, ServerCallContext context)
        {

            var user = await _vehicleRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Driver not found"));
            }

            var vehicleResponse = new VehicleResponse
            {
                Id = user.Id,
                PlateNumber = user.PlateNumber
            };

            return vehicleResponse;
        }
    }
}
