using Grpc.Core;
using RouteService.Domain.Interfaces.Repositories;  
using MyApp.Grpc.Route;
using RouteService.App.Converts;

namespace RouteService.Infrastructure.Proto.Service;

public class RouteGrpcService : RouteServiceGrpc.RouteServiceGrpcBase
{
    private readonly ILogger<RouteGrpcService> _logger;
    private readonly IRouteRepository _routeRepository;

    public RouteGrpcService(ILogger<RouteGrpcService> logger, IRouteRepository routeRepository)
    {
        _logger = logger;
        _routeRepository = routeRepository;
    }

    public override async Task<GetRouteResponse> GetRoute(GetRouteRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Received request for route with ID: {request.Id}");

        var route = await _routeRepository.GetByIdAsync(request.Id);
        if (route == null)
        {
            _logger.LogWarning($"Route with ID {request.Id} not found.");
            throw new RpcException(new Status(StatusCode.NotFound, $"Route with ID {request.Id} not found."));
        }

        var response = new GetRouteResponse
        {
            Route = RouteConverter.ToGrpcRoute(route)
        };

        _logger.LogInformation($"Route with ID {request.Id} retrieved successfully.");
        return response;
    }

    public override async Task<ListRoutesResponse> ListRoutes(ListRoutesRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Received request to list routes with filters.");

        var difficulty = request.DifficultyLevel != DifficultyLevelGrpc.Easy || request.DifficultyLevel != 0
            ? (Common.Shared.Enums.DifficultyLevel?)request.DifficultyLevel
            : null;

        var routes = await _routeRepository.ListAsync(new()
        {
            DifficultyLevel = difficulty,
            IsActive = request.IsActive,
            OriginName = request.OriginName,
            DestinationName = request.DestinationName
        });

        var response = new ListRoutesResponse();
        foreach (var route in routes)
        {
            response.Routes.Add(RouteConverter.ToGrpcRoute(route));
        }

        _logger.LogInformation($"Returned {response.Routes.Count} routes.");
        return response;
    }
}
