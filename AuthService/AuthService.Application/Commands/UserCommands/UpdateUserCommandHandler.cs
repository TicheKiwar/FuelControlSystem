using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using Common.Shared.message;
using MediatR;

namespace AuthService.AuthService.Application.Commands.UserCommands
{

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MessageResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<MessageResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(request.Id);
                if (existingUser == null)
                {
                    return new MessageResponse<User>("Usuario no encontrado");
                }

                existingUser.Username = request.Username;
                existingUser.Email = request.Email;
                existingUser.Roles = request.Roles;
                existingUser.RefreshToken = request.RefreshToken;
                existingUser.RefreshTokenExpiryTime = request.RefreshTokenExpiryTime;

                await _userRepository.UpdateAsync(existingUser);

                return new MessageResponse<User>(existingUser, "Usuario actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return new MessageResponse<User>($"Error al actualizar usuario: {ex.Message}");
            }
        }
    }
}
