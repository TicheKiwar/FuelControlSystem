using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.App.Commands.Trips
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, MessageResponse<Trip>>
    {
        private readonly ITripRepository _tripRepository;

        public CreateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<MessageResponse<Trip>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trip = new Trip
                {
                    DriverId = request.DriverId,
                    VehicleId = request.VehicleId,
                    RouteId = request.RouteId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    ExpectedFuelConsumed = request.ExpectedFuelConsumed,
                    TotalCost = request.TotalCost,
                    Status = request.Status,
                    IsEmergency = request.IsEmergency
                };

                await _tripRepository.AddAsync(trip);

                return new MessageResponse<Trip>(trip, "Viaje creado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<Trip>($"Error al crear viaje: {ex.Message}");
            }
        }
    }

}
