using Common.Shared.Enums;
using MongoDB.Driver;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Infrastructure.Data;

namespace RouteService.Infrastructure.Services
{
    public class TripService : ITripRepository
    {
        private readonly IMongoCollection<Trip> _tripCollection;

        public TripService(TripDbContext dbContext)
        {
            _tripCollection = dbContext.GetCollection<Trip>("Trips");
        }

        public async Task AddAsync(Trip trip)
        {
            await _tripCollection.InsertOneAsync(trip);
        }

        public async Task DeleteAsync(string id)
        {
            await _tripCollection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Trip>> GetActiveTripsAsync()
        {
            return await _tripCollection
                .Find(t => t.Status == TripStatus.Scheduled || t.Status == TripStatus.InProgress)
                .ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _tripCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Trip> GetByIdAsync(string id)
        {
            return await _tripCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> HasActiveTripForVehicleAsync(string vehicleId)
        {
            var filter = Builders<Trip>.Filter.And(
                Builders<Trip>.Filter.Eq(t => t.VehicleId, vehicleId),
                Builders<Trip>.Filter.In(t => t.Status, new[] { TripStatus.Scheduled, TripStatus.InProgress })
            );

            return await _tripCollection.Find(filter).AnyAsync();
        }

        public async Task UpdateAsync(Trip trip)
        {
            await _tripCollection.ReplaceOneAsync(t => t.Id == trip.Id, trip);
        }
    }

}
