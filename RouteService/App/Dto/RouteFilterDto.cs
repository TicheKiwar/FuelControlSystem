using Common.Shared.Enums;

namespace RouteService.Domain.Filters;

public class RouteFilter
{
    public DifficultyLevel? DifficultyLevel { get; set; }
    public bool? IsActive { get; set; }
    public string? OriginName { get; set; }
    public string? DestinationName { get; set; }
}
