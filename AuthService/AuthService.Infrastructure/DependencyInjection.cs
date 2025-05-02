using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using AuthService.AuthService.Infrastructure.Data.Persistence;
using AuthService.AuthService.Infrastructure.Data.Repositories;
using AuthService.AuthService.Infrastructure.Services;
using AuthService.AuthService.Api.Settings;
using AuthService.AuthService.Infrastructure.Settings;

namespace AuthService.AuthService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
                   this IServiceCollection services,
                   IConfiguration configuration)
        {
            // Configuración de MongoDB
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<FuelDbContext>();

            // Registrar repositorios
            services.AddScoped<IUserRepository, UserRepository>();

            // Registrar servicios
            services.AddScoped<IAuthService, AuthServiceImpl>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddHostedService<DataInitializer>();

            // Configuración JWT
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<AdminSettings>(configuration.GetSection("AdminSettings"));

            return services;
        }
    }

}
