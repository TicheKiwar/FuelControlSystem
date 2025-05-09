using Common.Shared.Enums;
using VehicleService.Domain.Entities;

namespace VehicleService.Domain.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(string id);

        Task<IEnumerable<Vehicle>> GetAllAsync();

        Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(VehicleType type);
        
        Task<IEnumerable<Vehicle>> GetVehiclesByFuelTypeAsync(FuelType fuelType);

        Task AddAsync(Vehicle vehicle);

        Task UpdateAsync(Vehicle vehicle);
        Task UpdateIsUnderMaintenance(string id, bool isUnderMaintenance);

        Task DeleteAsync(string id);

        Task<bool> ExistsByPlateNumberAsync(string plateNumber);

        Task CreateAsync(Vehicle vehicle);

        Task<List<Vehicle>> GetByTypeAsync(VehicleType type);

        Task<List<Vehicle>> FilterByBrandAndModelAsync(string brand, string model);

        Task<List<Vehicle>> GetByFuelTypeAsync(FuelType fuelType);
    }
}
