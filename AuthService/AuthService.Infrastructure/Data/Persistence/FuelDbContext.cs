using AuthService.AuthService.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthService.AuthService.Infrastructure.Data.Persistence
{
    public class FuelDbContext
    {
        private readonly IMongoDatabase _database;

        public FuelDbContext(IOptions<MongoDbSettings> settings)
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
            // Índices para la colección de usuarios
            var usersCollection = GetCollection<User>("users");
            var usernameIndex = new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Username),
                new CreateIndexOptions { Unique = true });

            var emailIndex = new CreateIndexModel<User>(
                Builders<User>.IndexKeys.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true });

            usersCollection.Indexes.CreateMany(new[] { usernameIndex, emailIndex });
        }
    }
}
