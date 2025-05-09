using Common.Shared.Enums;
using MongoDB.Driver;
using VehicleService.Domain.Entities;
using VehicleService.Domain.Interfaces.Repositories;
using VehicleService.Infrastructure.Data.Persistence;

namespace VehicleService.Infrastructure.Services
{
    public class VehicleServiceImp:IVehicleRepository
    {
        private readonly VehicleDbContext _context;

        public VehicleServiceImp(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.InsertOneAsync(vehicle);
        }

        public async Task CreateAsync(Vehicle vehicle)
        {
            await _context.Vehicles.InsertOneAsync(vehicle);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Vehicles.DeleteOneAsync(v => v.Id == id);
        }

        public async Task<bool> ExistsByPlateNumberAsync(string plateNumber)
        {
            var count = await _context.Vehicles.CountDocumentsAsync(v => v.PlateNumber == plateNumber);
            return count > 0;
        }

        public async Task<List<Vehicle>> FilterByBrandAndModelAsync(string brand, string model)
        {
            return await _context.Vehicles
                .Find(v => v.Brand == brand && v.Model == model)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.Find(_ => true).ToListAsync();
        }


        public async Task<List<Vehicle>> GetByFuelTypeAsync(FuelType fuelType)
        {
            return await _context.Vehicles
                .Find(v => v.FuelType == fuelType)
                .ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await _context.Vehicles
                .Find(v => v.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Vehicle>> GetByTypeAsync(VehicleType type)
        {
            return await _context.Vehicles
                .Find(v => v.Type == type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByFuelTypeAsync(FuelType fuelType)
        {
            return await GetByFuelTypeAsync(fuelType);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(VehicleType type)
        {
            return await GetByTypeAsync(type);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            await _context.Vehicles.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }

        public Task UpdateIsUnderMaintenance(string id, bool isUnderMaintenance)
        {
            throw new NotImplementedException();
        }
    }
}
