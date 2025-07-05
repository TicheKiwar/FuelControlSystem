using Common.Shared.App;
using Common.Shared.Enums;
using FuelService.Domain.Entities;
using FuelService.Domain.Interfaces;
using MongoDB.Driver;

namespace FuelService.Infrastructure.Persistence;

public class TripAuditRepository : ITripAuditRepository
{
    private readonly IMongoCollection<TripAudit> _collection;

    public TripAuditRepository(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDb:Database"]);
        _collection = database.GetCollection<TripAudit>("TripAudits");
    }

    public async Task InsertAsync(TripAudit audit)
    {
        // Calcular estimaciones antes de guardar
        await _collection.InsertOneAsync(audit);
    }

    public async Task<TripAudit> GetByTripIdAsync(string tripId)
    {
        return await _collection
            .Find(a => a.TripId == tripId)
            .SortByDescending(a => a.CalculatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<TripAudit[]> GetReportsAsync(string routeId, int skip = 0, int limit = 100)
    {
        return await _collection
            .Find(a => a.Inputs.Route.Id == routeId)
            .SortByDescending(a => a.CalculatedAt)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync()
            .ContinueWith(t => t.Result.ToArray());
    }

    public async Task UpdateFinalResultAsync(string tripId, FinalResult finalResult)
    {
        var filter = Builders<TripAudit>.Filter.Eq(a => a.TripId, tripId);
        var update = Builders<TripAudit>.Update
            .Set(a => a.FinalResult, finalResult)
            .Set(a => a.CalculatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(filter, update);
    }



}
