using Common.Models;
using Common.Shared.Settings;
using DriverServices.Domain.Interfaces.Repositories;
using DriverServices.Infrastructure.Data.Persistence;
using DriverServices.Infrastructure.Services;

namespace DriverServices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<DriverDbContext>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<IDriverRepository, DriverService>(); 


            return services;
        }
    }
}
