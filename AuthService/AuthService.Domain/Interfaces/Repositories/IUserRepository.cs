using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;

namespace AuthService.AuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<bool> CheckPasswordAsync(Guid userId, string password);
        Task AddRoleToUserAsync(Guid userId, UserRole role); 
        Task RemoveRoleFromUserAsync(Guid userId, UserRole role);
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);
        Task UpdateRefreshTokenAsync(string id, string refreshToken, DateTime dateTime);
    }
}
