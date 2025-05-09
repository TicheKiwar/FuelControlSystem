using VehicleService.Domain.Entities;

namespace VehicleService.Domain.Interfaces.Repositories
{
    public interface IVehicleMaintenance
    {
        Task<VehicleMaintenance> GetByIdAsync(string id);

        Task<IEnumerable<VehicleMaintenance>> GetAllAsync();

        Task<IEnumerable<VehicleMaintenance>> GetByVehicleIdAsync(string vehicleId);

        Task AddAsync(VehicleMaintenance maintenance);

        Task UpdateAsync(VehicleMaintenance maintenance);

        Task DeleteAsync(string id);

        Task<IEnumerable<VehicleMaintenance>> GetOngoingMaintenancesAsync();

        Task<IEnumerable<VehicleMaintenance>> GetCompletedMaintenancesAsync(string vehicleId);
    }
}
