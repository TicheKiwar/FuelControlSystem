using Common.Shared.Constants;
using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;

namespace RouteService.App.Commands.Routes
{
    public class UpdateRouteCommand : IRequest<MessageResponse<RouteEntity>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public double Distance { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public bool IsActive { get; set; }
        public List<Location> Waypoints { get; set; }

        public UpdateRouteCommand(
            string id,
            string name,
            Location origin,
            Location destination,
            double distance,
            double estimatedDuration,
            DifficultyLevel difficultyLevel,
            bool isActive,
            List<Location> waypoints)
        {
            Id = id;
            Name = name;
            Origin = origin;
            Destination = destination;
            Distance = distance;
            DifficultyLevel = difficultyLevel;
            IsActive = isActive;
            Waypoints = waypoints ?? new List<Location>();
        }
    }

}
