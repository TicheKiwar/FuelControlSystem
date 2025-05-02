using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;
using Common.Shared.message;
using MediatR;

namespace AuthService.AuthService.Application.Commands.UserCommands
{

    public class UpdateUserCommand : IRequest<MessageResponse<User>>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserRole> Roles { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public UpdateUserCommand(
            string id,
            string username,
            string email,
            List<UserRole> roles,
            string refreshToken,
            DateTime refreshTokenExpiryTime)
        {
            Id = id;
            Username = username;
            Email = email;
            Roles = roles ?? new List<UserRole>();
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }
}
