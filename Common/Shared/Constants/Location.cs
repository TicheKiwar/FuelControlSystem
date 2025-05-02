using MongoDB.Bson.Serialization.Attributes;

namespace Common.Shared.Constants
{
    public class Location
    {
        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }
    }
}
