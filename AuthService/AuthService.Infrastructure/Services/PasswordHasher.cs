using System.Security.Cryptography;
using System.Text;
using AuthService.AuthService.Application.Common.Interfaces;

namespace AuthService.AuthService.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public (byte[] Hash, byte[] Salt) CreateHash(string password)
        {
            using var hmac = new HMACSHA512();
            return (
                Hash: hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                Salt: hmac.Key
            );
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
