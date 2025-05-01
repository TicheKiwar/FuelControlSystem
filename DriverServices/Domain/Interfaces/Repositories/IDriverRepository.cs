using Common.Shared.Enums;
using DriverServices.Domain.Entities;

namespace DriverServices.Domain.Interfaces.Repositories
{
    public interface IDriverRepository
    {

            Task<Driver> GetByIdAsync(string id);
            Task<IEnumerable<Driver>> GetAllAsync();
            Task<IEnumerable<Driver>> GetAvailableDriversAsync();
            Task<IEnumerable<Driver>> GetDriversByMachineryTypeAsync(VehicleType type);
            Task AddAsync(Driver driver);
            Task UpdateAsync(Driver driver);
            Task DeleteAsync(string id);
            Task<bool> ExistsByDniAsync(string dni);
            Task SetAvailabilityAsync(string driverId, bool isAvailable);
        
    }
}
