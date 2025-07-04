using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.App.Commands.Routes
{
    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand, MessageResponse<RouteEntity>>
    {
        private readonly IRouteRepository _routeRepository;

        public UpdateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<MessageResponse<RouteEntity>> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var route = await _routeRepository.GetByIdAsync(request.Id);
                if (route == null)
                    return new MessageResponse<RouteEntity>("Ruta no encontrada");

                route.Name = request.Name;
                route.Origin = request.Origin;
                route.Destination = request.Destination;
                route.Distance = request.Distance;
                route.DifficultyLevel = request.DifficultyLevel;
                route.IsActive = request.IsActive;
                route.Waypoints = request.Waypoints;

                await _routeRepository.UpdateAsync(route);

                return new MessageResponse<RouteEntity>(route, "Ruta actualizada exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<RouteEntity>($"Error al actualizar ruta: {ex.Message}");
            }
        }
    }

}
