using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.App.Commands.Trips
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, MessageResponse<Trip>>
    {
        private readonly ITripRepository _tripRepository;

        public UpdateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<MessageResponse<Trip>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trip = await _tripRepository.GetByIdAsync(request.Id);
                if (trip == null)
                    return new MessageResponse<Trip>("Viaje no encontrado");

                trip.DriverId = request.DriverId;
                trip.VehicleId = request.VehicleId;
                trip.RouteId = request.RouteId;
                trip.StartTime = request.StartTime;
                trip.EndTime = request.EndTime;
                trip.ActualFuelConsumed = request.ActualFuelConsumed;
                trip.TotalCost = request.TotalCost;
                trip.Status = request.Status;
                trip.IsEmergency = request.IsEmergency;

                await _tripRepository.UpdateAsync(trip);

                return new MessageResponse<Trip>(trip, "Viaje actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<Trip>($"Error al actualizar viaje: {ex.Message}");
            }
        }
    }

}
