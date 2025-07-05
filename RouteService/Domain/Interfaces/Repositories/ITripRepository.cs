using Common.Shared.Enums;
using RouteService.App.Dto;
using RouteService.Domain.Entities;

namespace RouteService.Domain.Interfaces.Repositories
{
    public interface ITripRepository
    {
        Task<Trip> GetByIdAsync(string id);
        Task<IEnumerable<Trip>> ListAsync(TripFilterDto filter);
        Task<IEnumerable<Trip>> GetAllAsync();
        Task AddAsync(Trip trip);
        Task UpdateAsync(Trip trip);
        Task DeleteAsync(string id);

        Task<bool> HasActiveTripForVehicleAsync(string vehicleId);
        Task<IEnumerable<Trip>> GetActiveTripsAsync();
    }
}
