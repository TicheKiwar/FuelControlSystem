using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;

namespace AuthService.AuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(string id);
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task AddRoleToUserAsync(string userId, UserRole role); 
        Task RemoveRoleFromUserAsync(string userId, UserRole role);
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
        Task UpdateRefreshTokenAsync(string id, string refreshToken, DateTime dateTime);
        Task<IEnumerable <User>> GetAllAsync();
    }
}
