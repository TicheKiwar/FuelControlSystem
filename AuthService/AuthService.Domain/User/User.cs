using AuthService.AuthService.Domain.Roles;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AuthService.AuthService.Domain.User
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("passwordSalt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("roles")]
        [BsonRepresentation(BsonType.String)]
        public List<UserRole> Roles { get; set; } = new List<UserRole>();

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; }

        [BsonElement("refreshTokenExpiryTime")]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}