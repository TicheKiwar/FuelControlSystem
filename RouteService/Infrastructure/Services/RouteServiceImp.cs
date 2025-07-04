using Common.Shared.App;
using Common.Shared.Constants;
using MongoDB.Driver;
using RouteService.App.Interface;
using RouteService.Domain.Entities;
using RouteService.Domain.Filters;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Infrastructure.Data;
using RouteService.Infrastructure.Proto.Client;

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

        public async Task<IEnumerable<RouteEntity>> ListAsync(RouteFilter filter)
        {
            var filters = new List<FilterDefinition<RouteEntity>>();
            var builder = Builders<RouteEntity>.Filter;

            if (filter.DifficultyLevel.HasValue)
            {
                filters.Add(builder.Eq(r => r.DifficultyLevel, filter.DifficultyLevel.Value));
            }

            if (filter.IsActive.HasValue)
            {
                filters.Add(builder.Eq(r => r.IsActive, filter.IsActive.Value));
            }

            if (!string.IsNullOrWhiteSpace(filter.OriginName))
            {
                filters.Add(builder.Eq(r => r.Origin.Address, filter.OriginName));
            }

            if (!string.IsNullOrWhiteSpace(filter.DestinationName))
            {
                filters.Add(builder.Eq(r => r.Destination.Address, filter.DestinationName));
            }

            var finalFilter = filters.Any() ? builder.And(filters) : builder.Empty;

            return await _routeCollection.Find(finalFilter).ToListAsync();
        }


        public async Task UpdateAsync(RouteEntity route)
        {
            await _routeCollection.ReplaceOneAsync(r => r.Id == route.Id, route);
        }
    }

}
