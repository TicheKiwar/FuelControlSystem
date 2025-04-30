using MediatR;

namespace AuthService.AuthService.Application.Commands.Auth.Login
{
    public record LoginCommand : IRequest<AuthResponse>
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
    public record AuthResponse(string Token, string RefreshToken, DateTime? Expiration)
    {    }
}
