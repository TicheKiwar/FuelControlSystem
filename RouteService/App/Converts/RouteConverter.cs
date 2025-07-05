using MyApp.Grpc.Route;
using RouteService.Domain.Entities;
using Common.Shared.Enums;
using Common.Shared.Constants;

namespace RouteService.App.Converts;

public static class RouteConverter
{
    public static RouteEntityGrpc ToGrpcRoute(RouteEntity route)
    {
        return new RouteEntityGrpc
        {
            Id = route.Id,
            Name = route.Name,
            Origin = ToGrpcLocation(route.Origin),
            Destination = ToGrpcLocation(route.Destination),
            Distance = route.Distance,
            EstimatedDuration = route.EstimatedDuration,
            DifficultyLevel = (DifficultyLevelGrpc)route.DifficultyLevel,
            IsActive = route.IsActive,
            Waypoints = { route.Waypoints.Select(ToGrpcLocation) }
        };
    }

    private static LocationGrpc ToGrpcLocation(Location location)
    {
        return new LocationGrpc
        {
            Address = location.Address,
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };
    }
}
