using Common.Shared.message;
using MediatR;
using RouteService.App.Commands.Trips;
using RouteService.App.Interface;
using RouteService.Domain.Entities;

namespace RouteService.App.Behavior
{
    public class CheckIfVehicleExistsBehavior : IPipelineBehavior<CreateTripCommand, MessageResponse<Trip>>
    {
        private readonly IVehicleClient _vehicleClient;

        public CheckIfVehicleExistsBehavior(IVehicleClient vehicleClient)
        {
            _vehicleClient = vehicleClient;
        }

        public async Task<MessageResponse<Trip>> Handle(CreateTripCommand request, RequestHandlerDelegate<MessageResponse<Trip>> next, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _vehicleClient.GetVehicleAsync(request.VehicleId);
                if (vehicle == null)
                {
                    return new MessageResponse<Trip>
                    {
                        Success = false,
                        Message = $"El vehículo con ID '{request.VehicleId}' no existe."
                    };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Trip>
                {
                    Success = false,
                    Message = $"Error al validar el vehículo: {ex.Message}"
                };
            }

            return await next();
        }
    }
}
