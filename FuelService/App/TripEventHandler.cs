// Application/TripEventHandler.cs
using Common.Shared.App;
using Common.Shared.Enums;
using FuelService.App.Events;
using FuelService.Domain.Entities;
using FuelService.Domain.Interfaces;
using FuelService.Infrastructure;

namespace FuelService.App;

public class TripEventHandler
{
    private readonly ITripAuditRepository _repo;

    public TripEventHandler(ITripAuditRepository repo)
    {
        _repo = repo;
    }
    public async Task HandleAsync(TripAuditEvent message)
    {
        var inputs = new CalculationInputs
        {
            Driver = new DriverInput
            {
                Id = message.Driver.Id,
                FirstName = message.Driver.FirstName,
                LastName = message.Driver.LastName,
                HourlyRate = message.Driver.HourlyRate
            },
            Vehicle = new VehicleInput
            {
                Id = message.Vehicle.Id,
                PlateNumber = message.Vehicle.PlateNumber,
                FuelEfficiency = message.Vehicle.FuelEfficiency,
                AverageFuelEfficiency = message.Vehicle.AverageFuelEfficiency,
                IsUnderMaintenance = message.Vehicle.IsUnderMaintenance
            },
            Route = new RouteInput
            {
                Id = message.Route.Id,
                Name = message.Route.Name,
                Distance = message.Route.Distance,
                EstimatedDuration = message.Route.EstimatedDuration,
                DifficultyLevel = message.Route.DifficultyLevel,
                IsActive = message.Route.IsActive,
                Origin = new LocationInput
                {
                    Address = message.Route.Origin.Address,
                    Latitude = message.Route.Origin.Latitude,
                    Longitude = message.Route.Origin.Longitude
                },
                Destination = new LocationInput
                {
                    Address = message.Route.Destination.Address,
                    Latitude = message.Route.Destination.Latitude,
                    Longitude = message.Route.Destination.Longitude
                }
            }
        };

          var estimatedMinutes = TravelTimeCalculator.CalculateEstimatedTime(
            distance: inputs.Route.Distance,
            averageSpeed: double.Parse(inputs.Vehicle.AverageFuelEfficiency), // asegúrate que es un número válido
            difficultyLevel: Enum.TryParse<DifficultyLevel>(inputs.Route.DifficultyLevel, out var level) ? level : DifficultyLevel.Easy
        ) * 60;
            var calculationResult = new CalculationResult
        {
            EstimatedFuelLiters = inputs.Route.Distance / double.Parse(inputs.Vehicle.FuelEfficiency), // simplificado
            EstimatedDurationMinutes = estimatedMinutes,
            EstimatedPrice = double.Parse(inputs.Driver.HourlyRate) * (estimatedMinutes / 60)
        };


        var audit = new TripAudit
        {
            TripId = message.Trip.Id,
            CalculatedAt = DateTime.UtcNow,
            StartTime = message.Trip.StartTime,
            EndTime = message.Trip.EndTime,
            Inputs = inputs,
            CalculationResult = calculationResult,  
            FinalResult = message.Action == "trip_completed"
                ? new FinalResult
                {
                    ActualFuelConsumed = message.Trip.FinalResult?.ActualFuelConsumed,
                    ActualDurationMinutes = (message.Trip.EndTime - message.Trip.StartTime)?.TotalMinutes,
                    ActualPrice = message.Trip.FinalResult?.ActualPrice
                }
                : null
        };

        await _repo.InsertAsync(audit);
    }
}

