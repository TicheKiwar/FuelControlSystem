using RouteService.Domain.Proto.Client;

namespace RouteService.App.Interface
{
    public interface IVehicleClient
    {
        Task<VehicleDto> GetVehicleAsync(string vehicleId);
    }
}
