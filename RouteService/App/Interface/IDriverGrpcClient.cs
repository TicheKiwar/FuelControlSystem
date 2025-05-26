using RouteService.Infrastructure.Proto.Client;

namespace RouteService.App.Interface
{
    public interface IDriverGrpcClient
    {
        Task<DriverDto> GetDriverAsync(string driverId);
    }
}
