using Common.Models;
using Common.Shared.Settings;
using DriverServices.App.Behavior;
using DriverServices.App.Commands;
using DriverServices.Domain.Interfaces.Repositories;
using DriverServices.Infrastructure.Data.Persistence;
using DriverServices.Infrastructure.Services;
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
            services.AddSingleton<DriverDbContext>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddTransient<IPipelineBehavior<CreateDriverRegistrationCommand, CreateDriverRegistrationResult>, VerifyUserIdBehavior>();
            services.AddTransient<IPipelineBehavior<CreateDriverRegistrationCommand, CreateDriverRegistrationResult>, CheckIfUserAlreadyAssignedBehavior>();

            services.AddScoped<IDriverRepository, DriverService>();


            return services;
        }
    }
}
