using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using AuthService.AuthService.Infrastructure.Data.Persistence;
using AuthService.AuthService.Infrastructure.Data.Repositories;
using AuthService.AuthService.Infrastructure.Services;

namespace AuthService.AuthService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de MongoDB
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<FuelDbContext>();

            // Repositorios
            services.AddScoped<IUserRepository, UserRepository>();

            // Servicios
            services.AddScoped<IAuthService, AuthService>();
            services.AddHostedService<DataInitializer>();

            // Configuración JWT
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<AdminSettings>(configuration.GetSection("AdminSettings"));

            return services;
        }
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; } = 7;
    }

    public class AdminSettings
    {
        public bool Enabled { get; set; } = true;
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
