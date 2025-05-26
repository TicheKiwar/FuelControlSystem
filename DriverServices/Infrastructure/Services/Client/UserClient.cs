
using System;
using System.Threading.Tasks;
using DriverServices.App.Interfaces;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyApp.Grpc;

namespace DriverServices.Infrastructure.Services.Client
{
    public class UserClient : IUserClient
    {
        private readonly ILogger<UserClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly GrpcChannel _channel;

        public UserClient(ILogger<UserClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            // Obtener la URL del servicio Auth desde la configuración
            var authServiceUrl = _configuration.GetValue<string>("Services:AuthService");
            if (string.IsNullOrEmpty(authServiceUrl))
            {
                throw new ArgumentException("La URL del servicio Auth no está configurada.");
            }

            // Crear un canal gRPC
            _channel = GrpcChannel.ForAddress(authServiceUrl);
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            try
            {
                _logger.LogInformation($"Solicitando datos de usuario con ID: {userId}");

                // Crear el cliente gRPC
                var client = new UserService.UserServiceClient(_channel);

                // Realizar la solicitud
                var response = await client.GetUserAsync(new GetUserRequest { Id = userId });

                // Mapear la respuesta a un DTO
                var userDto = new UserDto
                {
                    Id = response.Id,
                    Username = response.Username,
                    Email = response.Email
                };

                _logger.LogInformation($"Datos de usuario obtenidos correctamente para el ID: {userId}");
                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener datos de usuario para ID: {userId}");
                throw;
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}