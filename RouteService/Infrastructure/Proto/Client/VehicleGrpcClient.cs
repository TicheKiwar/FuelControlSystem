using Grpc.Core;
using Grpc.Net.Client;
using MyApp.Grpc;
using RouteService.App.Dto;
using RouteService.App.Interface;
using static RouteService.Infrastructure.Proto.Client.VehicleClient;

namespace RouteService.Infrastructure.Proto.Client
{
    public class VehicleClient : IVehicleClient, IDisposable
    {
        private readonly ILogger<VehicleClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly GrpcChannel _channel;

        public VehicleClient(ILogger<VehicleClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            var vehicleServiceUrl = _configuration.GetValue<string>("Services:VehicleService");
            if (string.IsNullOrEmpty(vehicleServiceUrl))
            {
                throw new ArgumentException("La URL del servicio Vehicle no está configurada.");
            }

            _channel = GrpcChannel.ForAddress(vehicleServiceUrl);
        }

        public async Task<VehicleDto> GetVehicleAsync(string vehicleId)
        {
            try
            {
                _logger.LogInformation($"Solicitando datos del vehículo con ID: {vehicleId}");

                var client = new VehicleService.VehicleServiceClient(_channel);
                var response = await client.GetVehicleAsync(new GetVehicleRequest { Id = vehicleId });

                var vehicleDto = new VehicleDto
                {
                    Id = response.Id,
                    PlateNumber = response.PlateNumber,
                    FuelEfficiency = double.Parse(response.FuelEfficiency),
                    AverageFuelEfficiency = double.Parse(response.AverageFuelEfficiency),
                    IsUnderMaintenance = response.IsUnderMaintenance

                };

                _logger.LogInformation($"Datos del vehículo obtenidos correctamente para el ID: {vehicleId}");
                return vehicleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener datos del vehículo con ID: {vehicleId}");
                throw;
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
        }
    }

}
