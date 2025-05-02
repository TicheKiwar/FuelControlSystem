using Common.Shared.message;
using MediatR;
using RouteService.App.Commands.Trips;
using RouteService.Domain.Interfaces.Repositories;
using System.Linq;

namespace RouteService.App.Behavior
{
    public class TripAssignmentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ITripRepository _tripRepository;

        public TripAssignmentValidationBehavior(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is CreateTripCommand createCmd)
            {
                var conflict = await HasAssignmentConflict(createCmd.DriverId, createCmd.VehicleId);
                if (conflict != null)
                    return CreateErrorResponse(conflict);
            }

            if (request is UpdateTripCommand updateCmd)
            {
                var conflict = await HasAssignmentConflict(updateCmd.DriverId, updateCmd.VehicleId, updateCmd.Id);
                if (conflict != null)
                    return CreateErrorResponse(conflict);
            }

            return await next();
        }

        private async Task<string?> HasAssignmentConflict(string driverId, string vehicleId, string? currentTripId = null)
        {
            var activeTrips = await _tripRepository.GetActiveTripsAsync();

            var conflictingTrip = activeTrips.FirstOrDefault(t =>
                (t.DriverId == driverId || t.VehicleId == vehicleId) &&
                t.Id != currentTripId);

            if (conflictingTrip != null)
            {
                if (conflictingTrip.DriverId == driverId)
                    return $"El conductor con ID {driverId} ya está asignado al viaje {conflictingTrip.Id}";

                if (conflictingTrip.VehicleId == vehicleId)
                    return $"El vehículo con ID {vehicleId} ya está asignado al viaje {conflictingTrip.Id}";
            }

            return null;
        }

        private TResponse CreateErrorResponse(string errorMessage)
        {
            var responseType = typeof(TResponse);

            // Verifica si TResponse es MessageResponse<T>
            if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(MessageResponse<>))
            {
                // Usa el constructor que recibe un string
                return (TResponse)Activator.CreateInstance(responseType, errorMessage)!;
            }

            throw new InvalidOperationException($"No se pudo crear una respuesta de error para el tipo {responseType.Name}");
        }

    }
    }
