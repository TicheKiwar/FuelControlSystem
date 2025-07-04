using Common.Shared.Constants;
using Common.Shared.Enums;

namespace RouteService.App.Dto;

public class RouteDto
{

        public string Id { get; set; }

        public required string Name { get; set; } // Nombre descriptivo de la ruta

        public required Location Origin { get; set; } // Punto de origen

        public required Location Destination { get; set; } // Punto de destino

        public double Distance { get; set; } // Distancia en kilómetros

        public double EstimatedDuration { get; set; } // Duración estimada en horas

        public DifficultyLevel DifficultyLevel { get; set; } // Fácil, Medio, Difícil

        public bool IsActive { get; set; } = true; // Si la ruta está disponible

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Location> Waypoints { get; set; } = new List<Location>();
}
