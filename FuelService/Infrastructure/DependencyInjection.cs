using Common.Models;
using Common.Shared.message;
using Common.Shared.Settings;
using DriverServices.Infrastructure.Data.Persistence;
using FuelService.Domain.Entities;
using FuelService.Domain.Interfaces;
using FuelService.Infrastructure.Persistence;
using MediatR;


namespace DriverServices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<TripAuditContext>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<ITripAuditRepository, TripAuditRepository>();


            return services;
        }
    }
}
