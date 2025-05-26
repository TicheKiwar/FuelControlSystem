using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace VehicleService.Domain.Entities
{
    public class VehicleMaintenance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("vehicleId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VehicleId { get; set; }

        [BsonElement("startDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? EndDate { get; set; }

        [BsonElement("description")]
        public required string Description { get; set; }

        [BsonElement("cost")]
        public decimal Cost { get; set; }

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }

        [BsonElement("serviceProvider")]
        public string? ServiceProvider { get; set; }
    }
}
