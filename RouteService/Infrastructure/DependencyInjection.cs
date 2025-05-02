using Common.Models;
using Common.Shared.Settings;
using MediatR;
using RouteService.App.Behavior;
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TripAssignmentValidationBehavior<,>));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddScoped<ITripRepository, TripService>();
            services.AddScoped<IRouteRepository, RouteServiceImp>();


            return services;
        }
    }
}
