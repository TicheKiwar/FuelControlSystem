using DriverServices.Domain.Interfaces.Repositories;
using DriverServices.Domain.Entities;
using Common.Shared.Enums;
using MongoDB.Driver;
using DriverServices.Infrastructure.Data.Persistence;

namespace DriverServices.Infrastructure.Services
{
    public class DriverService : IDriverRepository
    {
        private readonly IMongoCollection<Driver> _drivers;

        public DriverService(DriverDbContext dbContext)
        {
            _drivers = dbContext.GetCollection<Driver>("drivers");
        }

        public async Task AddAsync(Driver driver)
        {
            await _drivers.InsertOneAsync(driver);
        }

        public async Task DeleteAsync(string id)
        {
            await _drivers.DeleteOneAsync(d => d.Id == id);
        }

        public async Task<bool> ExistsByDniAsync(string dni)
        {
            return await _drivers.Find(d => d.Dni == dni).AnyAsync();
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _drivers.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
        {
            return await _drivers.Find(d => d.IsAvailability && d.IsActive).ToListAsync();
        }

        public async Task<Driver> GetByIdAsync(string id)
        {
            return await _drivers.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Driver>> GetDriversByMachineryTypeAsync(VehicleType type)
        {
            return await _drivers.Find(d => d.MachineryType == type && d.IsAvailability && d.IsActive).ToListAsync();
        }

        public async Task SetAvailabilityAsync(string driverId, bool isAvailable)
        {
            var update = Builders<Driver>.Update.Set(d => d.IsAvailability, isAvailable);
            await _drivers.UpdateOneAsync(d => d.Id == driverId, update);
        }

        public async Task UpdateAsync(Driver driver)
        {
            await _drivers.ReplaceOneAsync(d => d.Id == driver.Id, driver);
        }
    }
}