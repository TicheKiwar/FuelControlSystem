using Common.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RouteService.Domain.Entities;

namespace RouteService.Infrastructure.Data
{
    public class RouteDbContext
    {
        private readonly IMongoDatabase _database;

        public RouteDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(
                "Routes");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
