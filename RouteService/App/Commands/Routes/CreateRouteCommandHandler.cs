using Common.Shared.App;
using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.App.Commands.Routes
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, MessageResponse<RouteEntity>>
    {
        private readonly IRouteRepository _routeRepository;

        public CreateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<MessageResponse<RouteEntity>> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var route = new RouteEntity
                {
                    Name = request.Name,
                    Origin = request.Origin,
                    Destination = request.Destination,
                    Distance = request.Distance,
                    DifficultyLevel = request.DifficultyLevel,
                    IsActive = request.IsActive,
                    Waypoints = request.Waypoints,
                    CreatedAt = DateTime.UtcNow
                };

                await _routeRepository.AddAsync(route);

                return new MessageResponse<RouteEntity>(route, "Ruta creada exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<RouteEntity>($"Error al crear ruta: {ex.Message}");
            }
        }
    }

}
