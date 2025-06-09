using DriverServices.Domain.Interfaces.Repositories;
using Grpc.Core;
using MyApp.Grpc;

namespace DriverServices.Infrastructure.Services.Proto
{
    public class DriverGrpcService : MyApp.Grpc.DriverService.DriverServiceBase
    {
        private readonly IDriverRepository _driverRepository;


        public DriverGrpcService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public override async Task<DriverResponse> GetDriver(GetDriverRequest request, ServerCallContext context)
        {

            var user = await _driverRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Driver not found"));
            }

            var driverResponse = new DriverResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HourlyRate =  user.HourlyRate.ToString(),
            };

            return driverResponse;
        }
    }
}
