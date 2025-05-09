using Common.Models;
using Common.Shared.Settings;
using MediatR;
using VehicleService.Domain.Interfaces.Repositories;
using VehicleService.Infrastructure.Data.Persistence;
using VehicleService.Infrastructure.Services;

namespace DriverServices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<VehicleDbContext>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IVehicleRepository, VehicleServiceImp>();
            services.AddScoped<IVehicleMaintenance, VehicleMaintenanceServiceImp>();

            return services;
        }
    }
}
