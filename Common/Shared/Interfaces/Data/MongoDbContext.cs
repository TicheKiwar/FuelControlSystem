using Common.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Common.Shared.Interfaces.Data
{
        public class MongoDbContext : IDbContext
        {
            private readonly IMongoDatabase _database;

            public MongoDbContext(IOptions<MongoDbSettings> settings)
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.DatabaseName);
            }

            public IMongoCollection<T> GetCollection<T>(string name)
            {
                return _database.GetCollection<T>(name);
            }

            public void EnsureIndexes<T>(string collectionName)
            {
                // Método base vacío para ser sobrescrito por implementaciones específicas
            }
        }
    
}
