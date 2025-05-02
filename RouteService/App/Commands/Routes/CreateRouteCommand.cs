using Common.Shared.Constants;
using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using RouteService.Domain.Entities;

namespace RouteService.App.Commands.Routes
{
    public class CreateRouteCommand : IRequest<MessageResponse<RouteEntity>>
    {
        public string Name { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public double Distance { get; set; }
        public double EstimatedDuration { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public bool IsActive { get; set; }
        public List<Location> Waypoints { get; set; }

        public CreateRouteCommand(
            string name,
            Location origin,
            Location destination,
            double distance,
            double estimatedDuration,
            DifficultyLevel difficultyLevel,
            bool isActive,
            List<Location> waypoints)
        {
            Name = name;
            Origin = origin;
            Destination = destination;
            Distance = distance;
            EstimatedDuration = estimatedDuration;
            DifficultyLevel = difficultyLevel;
            IsActive = isActive;
            Waypoints = waypoints ?? new List<Location>();
        }
    }
}
