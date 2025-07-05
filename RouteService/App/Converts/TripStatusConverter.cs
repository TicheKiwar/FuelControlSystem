using Common.Shared.Enums;
using MyApp.Grpc.trip;

namespace RouteService.App.Converts;

public static class TripStatusConverter
{
    public static TripStatusGrpc ToGrpcTripStatus(TripStatus status)
    {
        return status switch
        {
            TripStatus.Scheduled => TripStatusGrpc.Scheduled,
            TripStatus.InProgress => TripStatusGrpc.InProgress,
            TripStatus.Completed => TripStatusGrpc.Completed,
            TripStatus.Cancelled => TripStatusGrpc.Cancelled,
            TripStatus.Delayed => TripStatusGrpc.Delayed,
            _ => TripStatusGrpc.Unspecified
        };
    }

    public static TripStatus FromGrpcTripStatus(TripStatusGrpc grpcStatus)
    {
        return grpcStatus switch
        {
            TripStatusGrpc.Scheduled => TripStatus.Scheduled,
            TripStatusGrpc.InProgress => TripStatus.InProgress,
            TripStatusGrpc.Completed => TripStatus.Completed,
            TripStatusGrpc.Cancelled => TripStatus.Cancelled,
            TripStatusGrpc.Delayed => TripStatus.Delayed,
            _ => throw new ArgumentOutOfRangeException(nameof(grpcStatus), grpcStatus, "Unsupported gRPC TripStatusGrpc value")
        };
    }
}
