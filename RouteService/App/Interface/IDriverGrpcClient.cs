using RouteService.Infrastructure.Proto.Client;

namespace RouteService.App.Interface
{
    public interface IDriverGrpcClient : IDisposable
    {
        Task<DriverDto> GetDriverAsync(string driverId);
    }
}
