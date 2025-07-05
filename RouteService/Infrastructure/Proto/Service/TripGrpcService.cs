using Grpc.Core;
using MyApp.Grpc.trip;
using RouteService.App.Converts;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Infrastructure.Proto.Service;

public class TripGrpcService : TripService.TripServiceBase
{
    private readonly ILogger<TripGrpcService> _logger;
    private readonly ITripRepository _tripRepository;

    public TripGrpcService(ILogger<TripGrpcService> logger, ITripRepository tripRepository)
    {
        _logger = logger;
        _tripRepository = tripRepository;
    }


    public override async Task<TripResponse> GetTrip(GetTripRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received request for trip with ID: {request.Id}");

        var trip = await _tripRepository.GetByIdAsync(request.Id);
        if (trip == null)
        {
            _logger.LogWarning($"Trip with ID {request.Id} not found.");
            throw new RpcException(new Status(StatusCode.NotFound, $"Trip with ID {request.Id} not found."));
        }

        var response = new TripResponse
        {
            Trip = {
                Id = trip.Id,
                VehicleId = trip.VehicleId,
                DriverId = trip.DriverId,
                RouteId = trip.RouteId,
                StartTime = trip.StartTime.ToUniversalTime().ToString("o"),
                EndTime = trip.EndTime.HasValue ? trip.EndTime.Value.ToUniversalTime().ToString("o") : "",
                ExpectedFuelConsumed = trip.ExpectedFuelConsumed,
                ActualFuelConsumed = trip.ActualFuelConsumed ?? 0,
                Status = TripStatusConverter.ToGrpcTripStatus(trip.Status),
                IsEmergency = trip.IsEmergency,
            }
        };

        _logger.LogInformation($"Trip with ID {request.Id} retrieved successfully.");
        return response;
    }

    public override async Task<ListTripsResponse> ListTrips(ListTripsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Received request to list trips with filters.");

        // Parseo de fechas si están presentes
        DateTime? startTimeFrom = null;
        DateTime? startTimeTo = null;
        if (!string.IsNullOrEmpty(request.StartTimeFrom))
        {
            if (DateTime.TryParse(request.StartTimeFrom, out var parsedFrom))
            {
                startTimeFrom = parsedFrom.ToUniversalTime();
            }
        }

        if (!string.IsNullOrEmpty(request.StartTimeTo))
        {
            if (DateTime.TryParse(request.StartTimeTo, out var parsedTo))
            {
                startTimeTo = parsedTo.ToUniversalTime();
            }
        }

        // Obtener lista del repositorio con filtros
        var trips = await _tripRepository.ListAsync(new()
        {
            DriverId = request.DriverId,
            VehicleId = request.VehicleId,
            RouteId = request.RouteId,
            Status = (Common.Shared.Enums.TripStatus?)(
                request.Status != TripStatusGrpc.Unspecified ? (int)request.Status : (int?)null),
            IsEmergency = request.IsEmergency,
            StartTimeFrom = startTimeFrom,
            StartTimeTo = startTimeTo
        });

        var response = new ListTripsResponse();
        foreach (var trip in trips)
        {
            response.Trips.Add(new Trip
            {
                Id = trip.Id,
                VehicleId = trip.VehicleId,
                DriverId = trip.DriverId,
                RouteId = trip.RouteId,
                StartTime = trip.StartTime.ToUniversalTime().ToString("o"),
                EndTime = trip.EndTime.HasValue ? trip.EndTime.Value.ToUniversalTime().ToString("o") : "",
                ExpectedFuelConsumed = trip.ExpectedFuelConsumed,
                ActualFuelConsumed = trip.ActualFuelConsumed ?? 0,
                Status = TripStatusConverter.ToGrpcTripStatus(trip.Status),
                IsEmergency = trip.IsEmergency
            });
        }

        _logger.LogInformation($"Returned {response.Trips.Count} trips.");
        return response;
    }
}