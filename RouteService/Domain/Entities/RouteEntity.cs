using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Common.Shared.Enums;
using Common.Shared.Constants;

namespace RouteService.Domain.Entities
{
    public class RouteEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; } // Nombre descriptivo de la ruta

        [BsonElement("origin")]
        public required Location Origin { get; set; } // Punto de origen

        [BsonElement("destination")]
        public required Location Destination { get; set; } // Punto de destino

        [BsonElement("distance")]
        public double Distance { get; set; } // Distancia en kilómetros

        [BsonElement("estimatedDuration")]
        public double EstimatedDuration { get; set; } // Duración estimada en horas

        [BsonElement("difficultyLevel")]
        public DifficultyLevel DifficultyLevel { get; set; } // Fácil, Medio, Difícil

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true; // Si la ruta está disponible

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("waypoints")]
        public List<Location> Waypoints { get; set; } = new List<Location>(); // Puntos intermedios
    }
}
