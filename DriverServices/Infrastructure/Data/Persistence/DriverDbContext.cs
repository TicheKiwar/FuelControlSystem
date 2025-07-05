
using Common.Shared.Settings;
using DriverServices.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriverServices.Infrastructure.Data.Persistence
{
    public class DriverDbContext
    {
        private readonly IMongoDatabase _database;

        public DriverDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            EnsureIndexes();
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        private void EnsureIndexes()
        {
            var driversCollection = GetCollection<Driver>("drivers");

            var dniIndex = new CreateIndexModel<Driver>(
                Builders<Driver>.IndexKeys.Ascending(d => d.Dni),
                new CreateIndexOptions { Unique = true });

            var licenseIndex = new CreateIndexModel<Driver>(
                Builders<Driver>.IndexKeys.Ascending(d => d.License),
                new CreateIndexOptions { Unique = false });

            var userIndex = new CreateIndexModel<Driver>(
                Builders<Driver>.IndexKeys.Ascending(d => d.UserId),
                new CreateIndexOptions { Unique = false });

            driversCollection.Indexes.CreateMany(new[] { dniIndex, licenseIndex, userIndex });
        }
    }
}
