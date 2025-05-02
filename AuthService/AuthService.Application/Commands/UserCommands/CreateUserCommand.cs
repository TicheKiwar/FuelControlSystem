using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Enums;
using Common.Shared.message;
using MediatR;

namespace AuthService.AuthService.Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<MessageResponse<User>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Cambiado a texto plano
        public List<UserRole> Roles { get; set; }

        public CreateUserCommand(
            string username,
            string email,
            string password,  // Ahora recibe la contraseña normal
            List<UserRole> roles)
        {
            Username = username;
            Email = email;
            Password = password;
            Roles = roles ?? new List<UserRole>();
        }
    }
}