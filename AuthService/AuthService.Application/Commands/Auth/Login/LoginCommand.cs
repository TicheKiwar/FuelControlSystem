using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace AuthService.AuthService.Application.Commands.Auth.Login
{
    public record LoginCommand : IRequest<AuthResponse>
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
    public record AuthResponse(string Token, string RefreshToken, DateTime Expiration)
    {
        private DateTime? expiration;

        public AuthResponse(string? token, string? refreshToken, DateTime? expiration)
        {
            Token = token;
            RefreshToken = refreshToken;
            this.expiration = expiration;
        }
    }
}
