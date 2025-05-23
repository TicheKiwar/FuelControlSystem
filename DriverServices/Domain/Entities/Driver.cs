﻿using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Common.Shared.Enums;

namespace DriverServices.Domain.Entities
{
    public class Driver
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public required string FirstName { get; set; }

        [BsonElement("lastName")]
        public required string LastName { get; set; }

        [BsonElement("dni")]
        public required string Dni { get; set; }

        [BsonElement("address")]
        public required string Address { get; set; }

        [BsonElement("hourlyRate")]
        public required decimal HourlyRate { get; set; } // Ej: 15.50 (USD/hora o moneda local)

        [BsonElement("phone")]
        public required string Phone { get; set; }

        [BsonElement("isAvailability")]
        public bool IsAvailability { get; set; } = true;

        [BsonElement("isActive")]
        public required bool IsActive { get; set; }

        [BsonElement("license")]
        public required LicenseType License { get; set; }

        [BsonElement("machineryType")]
        public required VehicleType MachineryType { get; set; }

    }
}

