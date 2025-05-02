using AuthService.AuthService.Domain.Entities;

namespace AuthService.AuthService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> Authenticate(User user, string password);
        Task Register(User user, string password);
        Task<string> GenerateJwtToken(User user);
        Task<string> GenerateRefreshToken();
        Task<AuthResult> RefreshToken(string token, string refreshToken);
    }

    public record AuthResult(bool Success, string? Token = null, string? RefreshToken = null, DateTime? Expiration = null);
}
