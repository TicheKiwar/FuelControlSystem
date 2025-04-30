using AuthService.AuthService.Api.Settings;
using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;

namespace AuthService.AuthService.Infrastructure.Services
{
    public class DataInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminSettings _adminSettings;

        public DataInitializer(IServiceProvider serviceProvider, IOptions<AdminSettings> adminSettings)
        {
            _serviceProvider = serviceProvider;
            _adminSettings = adminSettings.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_adminSettings.Enabled) return;

            using var scope = _serviceProvider.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            await CreateAdminUser(userRepository, authService);
        }

        private async Task CreateAdminUser(IUserRepository userRepository, IAuthService authService)
        {
            var adminExists = await userRepository.GetByEmailAsync(_adminSettings.Email) != null;

            if (!adminExists)
            {
                User adminUser = new User
                {
                    Username = _adminSettings.Username,
                    Email = _adminSettings.Email,
                    Roles = new List<UserRole> { UserRole.Admin }
                };

                await authService.Register(adminUser, _adminSettings.Password);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
