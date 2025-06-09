using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Net.NetworkInformation;
using Common.Shared.Enums;

namespace RouteService.Domain.Entities
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Referencias
        [BsonElement("driverId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string DriverId { get; set; }

        [BsonElement("vehicleId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VehicleId { get; set; }

        [BsonElement("routeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string RouteId { get; set; }

        // Datos del viaje
        [BsonElement("startTime")]
        public required DateTime StartTime { get; set; }

        [BsonElement("endTime")]
        public DateTime? EndTime { get; set; }

        [BsonElement("expectedFuelConsumed")]
        public required double ExpectedFuelConsumed { get; set; }

        [BsonElement("actualFuelConsumed")]
        public double? ActualFuelConsumed { get; set; }

        [BsonElement("totalCost")]
        public decimal? TotalCost { get; set; } 

        [BsonElement("status")]
        public TripStatus Status { get; set; } = TripStatus.Scheduled;

        [BsonElement("isEmergency")]
        public bool IsEmergency { get; set; } = false;
    }

}
