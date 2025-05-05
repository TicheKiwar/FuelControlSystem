using Common.Models;
using Common.Shared.message;
using Common.Shared.Settings;
using MediatR;
using RouteService.App.Behavior;
using RouteService.App.Commands.Trips;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Infrastructure.Data;
using RouteService.Infrastructure.Services;

namespace DriverServices.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<RouteDbContext>();
            services.AddSingleton<TripDbContext>();

            services.AddTransient<IPipelineBehavior<CreateTripCommand, MessageResponse<Trip>>, CheckIfDriverExistsBehavior>();
            services.AddTransient<IPipelineBehavior<CreateTripCommand, MessageResponse<Trip>>, CheckIfVehicleExistsBehavior>();
            services.AddTransient<IPipelineBehavior<CreateTripCommand, MessageResponse<Trip>>, TripAssignmentValidationBehavior<CreateTripCommand, MessageResponse<Trip>>>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<ITripRepository, TripService>();
            services.AddScoped<IRouteRepository, RouteServiceImp>();


            return services;
        }
    }
}
