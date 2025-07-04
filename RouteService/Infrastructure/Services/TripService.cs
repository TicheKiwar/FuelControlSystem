using Common.Shared.Enums;
using MongoDB.Driver;
using RouteService.App.Dto;
using RouteService.App.Interface;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Infrastructure.Data;

namespace RouteService.Infrastructure.Services
{
    public class TripService : ITripRepository
    {
        private readonly IMongoCollection<Trip> _tripCollection;

        private readonly ITripEventPublisher _publisher;
        public TripService(TripDbContext dbContext, ITripEventPublisher publisher)
        {
            _tripCollection = dbContext.GetCollection<Trip>("Trips");
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public async Task AddAsync(Trip trip)
        {
            await _tripCollection.InsertOneAsync(trip);

            await _publisher.PublishAsync("created", trip);
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

        public async Task<IEnumerable<Trip>> ListAsync(TripFilterDto filter)
        {
            var builder = Builders<Trip>.Filter;
            var filterList = new List<FilterDefinition<Trip>>();

            if (!string.IsNullOrEmpty(filter.DriverId))
                filterList.Add(builder.Eq(t => t.DriverId, filter.DriverId));

            if (!string.IsNullOrEmpty(filter.VehicleId))
                filterList.Add(builder.Eq(t => t.VehicleId, filter.VehicleId));

            if (!string.IsNullOrEmpty(filter.RouteId))
                filterList.Add(builder.Eq(t => t.RouteId, filter.RouteId));

            if (filter.Status.HasValue)
                filterList.Add(builder.Eq(t => t.Status, filter.Status.Value));

            if (filter.IsEmergency.HasValue)
                filterList.Add(builder.Eq(t => t.IsEmergency, filter.IsEmergency.Value));

            if (filter.StartTimeFrom.HasValue)
                filterList.Add(builder.Gte(t => t.StartTime, filter.StartTimeFrom.Value));

            if (filter.StartTimeTo.HasValue)
                filterList.Add(builder.Lte(t => t.StartTime, filter.StartTimeTo.Value));

            var combinedFilter = filterList.Any() ? builder.And(filterList) : builder.Empty;

            return await _tripCollection.Find(combinedFilter).ToListAsync();
        }


        public async Task UpdateAsync(Trip trip)
        {
            await _tripCollection.ReplaceOneAsync(t => t.Id == trip.Id, trip);

            await _publisher.PublishAsync("updated", trip);
        }
    }

}
