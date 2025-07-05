
using Common.Shared.Settings;
using FuelService.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriverServices.Infrastructure.Data.Persistence
{
    public class TripAuditContext
    {
        private readonly IMongoDatabase _database;

        public TripAuditContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            EnsureIndexes();
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);/*  */
        }

        private void EnsureIndexes()
        {
            var driversCollection = GetCollection<TripAudit>("TripAudits");

            var TripIndex = new CreateIndexModel<TripAudit>(
                Builders<TripAudit>.IndexKeys.Ascending(d => d.TripId),
                new CreateIndexOptions { Unique = true });

            driversCollection.Indexes.CreateMany(new[] { TripIndex});
        }
    }
}
