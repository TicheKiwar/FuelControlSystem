using RouteService.App.Dto;

namespace RouteService.App.Interface
{
    public interface IVehicleClient : IDisposable
    {
        Task<VehicleDto> GetVehicleAsync(string vehicleId);
    }
}
