using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using Common.Shared.message;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.AuthService.Application.Commands.UserCommands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MessageResponse<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<MessageResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verificar si el email ya existe
                if (await _userRepository.ExistsByEmailAsync(request.Email))
                {
                    return new MessageResponse<User>("El email ya está registrado");
                }

                // Generar hash y salt
                var (hash, salt) = _passwordHasher.CreateHash(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Roles = request.Roles,
                    RefreshToken = string.Empty,
                    RefreshTokenExpiryTime = DateTime.MinValue
                };

                await _userRepository.AddAsync(user);

                return new MessageResponse<User>(user, "Usuario creado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<User>($"Error al crear usuario: {ex.Message}");
            }
        }
    }

}
