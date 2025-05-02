
using MongoDB.Driver;

namespace Common.Shared.Interfaces
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void EnsureIndexes<T>(string collectionName);
    }
}
