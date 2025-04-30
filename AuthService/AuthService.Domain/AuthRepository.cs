using AuthService.AuthService.Domain.User;

namespace AuthService.AuthService.Domain
{
    public interface AuthRepository
    {
        Task<User> GetUserById(string id);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(string id);
    }
}
