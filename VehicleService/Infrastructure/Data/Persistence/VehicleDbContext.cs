using Common.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VehicleService.Domain.Entities;

namespace VehicleService.Infrastructure.Data.Persistence
{
    public class VehicleDbContext
    {
        private readonly IMongoDatabase _database;

        public VehicleDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            EnsureIndexes();
        }

        public IMongoCollection<Vehicle> Vehicles => _database.GetCollection<Vehicle>("vehicles");

        private void EnsureIndexes()
        {
            var vehiclesCollection = Vehicles;

            var plateNumberIndex = new CreateIndexModel<Vehicle>(
                Builders<Vehicle>.IndexKeys.Ascending(v => v.PlateNumber),
                new CreateIndexOptions { Unique = true });


            vehiclesCollection.Indexes.CreateMany(new[] { plateNumberIndex });
        }
    }

}
