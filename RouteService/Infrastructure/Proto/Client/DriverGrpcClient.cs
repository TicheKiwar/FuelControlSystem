
using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyApp.Grpc;
using RouteService.App.Dto;
using RouteService.App.Interface;

namespace RouteService.Infrastructure.Proto.Client
{
    public class DriverGrpcClient : IDriverGrpcClient, IDisposable
    {
        private readonly ILogger<DriverGrpcClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly GrpcChannel _channel;

        public DriverGrpcClient(ILogger<DriverGrpcClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var driverServiceUrl = _configuration.GetValue<string>("Services:DriverService");
            if (string.IsNullOrEmpty(driverServiceUrl))
            {
                throw new ArgumentException("La URL del servicio Driver no está configurada.");
            }

            _channel = GrpcChannel.ForAddress(driverServiceUrl);
        }

        public async Task<DriverDto> GetDriverAsync(string driverId)
        {
            try
            {
                _logger.LogInformation($"Solicitando datos del conductor con ID: {driverId}");

                var client = new DriverService.DriverServiceClient(_channel);
                var response = await client.GetDriverAsync(new GetDriverRequest { Id = driverId });

                var driverDto = new DriverDto
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    HourlyRate = response.HourlyRate
                };

                _logger.LogInformation($"Datos del conductor obtenidos correctamente para el ID: {driverId}");
                return driverDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener datos del conductor con ID: {driverId}");
                throw;
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }


}

