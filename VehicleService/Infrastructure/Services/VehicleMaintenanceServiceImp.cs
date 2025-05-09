using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;
using VehicleService.Infrastructure.Data.Persistence;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace VehicleService.Infrastructure.Services
{
    public class VehicleMaintenanceServiceImp: IVehicleMaintenance
    {
        private readonly VehicleDbContext _context;

        public VehicleMaintenanceServiceImp(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VehicleMaintenance maintenance)
        {
            await _context.VehicleMaintenances.InsertOneAsync(maintenance);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.VehicleMaintenances.DeleteOneAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<VehicleMaintenance>> GetAllAsync()
        {
            return await _context.VehicleMaintenances.Find(_ => true).ToListAsync();
        }

        public async Task<VehicleMaintenance> GetByIdAsync(string id)
        {
            return await _context.VehicleMaintenances
                .Find(m => m.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<VehicleMaintenance>> GetByVehicleIdAsync(string vehicleId)
        {
            return await _context.VehicleMaintenances
                .Find(m => m.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleMaintenance>> GetCompletedMaintenancesAsync(string vehicleId)
        {
            return await _context.VehicleMaintenances
                .Find(m => m.VehicleId == vehicleId && m.IsCompleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleMaintenance>> GetOngoingMaintenancesAsync()
        {
            return await _context.VehicleMaintenances
                .Find(m => !m.IsCompleted)
                .ToListAsync();
        }

        public async Task UpdateAsync(VehicleMaintenance maintenance)
        {
            await _context.VehicleMaintenances
                .ReplaceOneAsync(m => m.Id == maintenance.Id, maintenance);
        }
    }
    
}
