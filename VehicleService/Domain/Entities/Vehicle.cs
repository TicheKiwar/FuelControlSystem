using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Common.Shared.Enums;

namespace VehicleService.Domain.Entities
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("plateNumber")]
        public required string PlateNumber { get; set; } // Ej: "ABC-123"

        [BsonElement("type")]
        public required VehicleType Type { get; set; } // Liviano/Pesado

        [BsonElement("fuelType")]
        public required FuelType FuelType { get; set; } // Gasolina, Diésel, etc.

        [BsonElement("fuelEfficiency")]
        public double FuelEfficiency { get; set; } // Consumo en km/L o km/kWh

        [BsonElement("brand")]
        public required string Brand { get; set; } // Ej: "Toyota"

        [BsonElement("model")]
        public required string Model { get; set; }
    }
}
