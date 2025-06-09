using Common.Shared.Constants;
using Common.Shared.Enums;
using RouteService.Domain.Entities;
using RouteService.Domain.Filters;

namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<RouteEntity> GetByIdAsync(string id);
        Task<IEnumerable<RouteEntity>> GetAllAsync();
        Task AddAsync(RouteEntity route);
        Task UpdateAsync(RouteEntity route);
        Task DeleteAsync(string id);

        // Métodos que la entidad Route "comprende" directamente
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<RouteEntity>> GetRoutesByLocationAsync(Location location);
        Task<IEnumerable<RouteEntity>> ListAsync(RouteFilter value);
    }
}
