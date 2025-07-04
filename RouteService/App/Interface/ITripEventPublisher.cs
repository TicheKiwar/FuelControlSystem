// Application/Interfaces/ITripEventPublisher.cs
using RouteService.Domain.Entities;

namespace RouteService.App.Interface;
public interface ITripEventPublisher
{
    Task PublishAsync(string eventType, Trip trip);
}
