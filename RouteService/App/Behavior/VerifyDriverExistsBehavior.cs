    using MediatR;
    using Grpc.Core;
    using RouteService.App.Interface;
using RouteService.App.Commands.Trips;
using Common.Shared.message;
using RouteService.Domain.Entities;

namespace RouteService.App.Behavior
{
    public class CheckIfDriverExistsBehavior : IPipelineBehavior<CreateTripCommand, MessageResponse<Trip>>
    {
        private readonly IDriverGrpcClient _driverClient;

        public CheckIfDriverExistsBehavior(IDriverGrpcClient driverClient)
        {
            _driverClient = driverClient;
        }

        public async Task<MessageResponse<Trip>> Handle(CreateTripCommand request, RequestHandlerDelegate<MessageResponse<Trip>> next, CancellationToken cancellationToken)
        {
            try
            {
                var driver = await _driverClient.GetDriverAsync(request.DriverId);
                if (driver == null)
                {
                    return new MessageResponse<Trip>
                    {
                        Success = false,
                        Message = $"El conductor con ID '{request.DriverId}' no existe."
                    };
                }
            }
            catch (Exception ex)
            {
                return new MessageResponse<Trip>
                {
                    Success = false,
                    Message = $"Error al validar el conductor: {ex.Message}"
                };
            }

            return await next();
        }
    }

}
