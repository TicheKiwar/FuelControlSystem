using Common.Shared.Constants;
using MongoDB.Driver;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Infrastructure.Data;

namespace RouteService.Infrastructure.Services
{
    public class RouteServiceImp : IRouteRepository
    {
        private readonly IMongoCollection<RouteEntity> _routeCollection;

        public RouteServiceImp(RouteDbContext dbContext)
        {
            _routeCollection = dbContext.GetCollection<RouteEntity>("Routes");
        }

        public async Task AddAsync(RouteEntity route)
        {
            await _routeCollection.InsertOneAsync(route);
        }

        public async Task DeleteAsync(string id)
        {
            await _routeCollection.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var filter = Builders<RouteEntity>.Filter.Eq(r => r.Name, name);
            return await _routeCollection.Find(filter).AnyAsync();
        }

        public async Task<IEnumerable<RouteEntity>> GetAllAsync()
        {
            return await _routeCollection.Find(_ => true).ToListAsync();
        }

        public async Task<RouteEntity> GetByIdAsync(string id)
        {
            return await _routeCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RouteEntity>> GetRoutesByLocationAsync(Location location)
        {
            var filter = Builders<RouteEntity>.Filter.Or(
                Builders<RouteEntity>.Filter.Eq(r => r.Origin, location),
                Builders<RouteEntity>.Filter.Eq(r => r.Destination, location),
                Builders<RouteEntity>.Filter.AnyEq(r => r.Waypoints, location)
            );

            return await _routeCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(RouteEntity route)
        {
            await _routeCollection.ReplaceOneAsync(r => r.Id == route.Id, route);
        }
    }

}
