using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using AuthService.AuthService.Infrastructure.Data.Persistence;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.AuthService.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(FuelDbContext context)
        {
            _users = context.GetCollection<User>("users");
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).AnyAsync();
        }

        public async Task AddAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.Id == id);
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await GetByIdAsync(userId);
            if (user == null) return false;

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(user.PasswordHash);
        }

        public async Task AddRoleToUserAsync(string userId, UserRole role)
        {
            var update = Builders<User>.Update.AddToSet(u => u.Roles, role);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task RemoveRoleFromUserAsync(string userId, UserRole role)
        {
            var update = Builders<User>.Update.Pull(u => u.Roles, role);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _users.Find(u => u.Roles.Contains(role)).ToListAsync();
        }

        public async Task UpdateRefreshTokenAsync(string userId, string refreshToken, DateTime expiryTime)
        {
            var update = Builders<User>.Update
                .Set(u => u.RefreshToken, refreshToken)
                .Set(u => u.RefreshTokenExpiryTime, expiryTime);

            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(Guid userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task AddRoleToUserAsync(Guid userId, UserRole role)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRoleFromUserAsync(Guid userId, UserRole role)
        {
            throw new NotImplementedException();
        }
    }
}
