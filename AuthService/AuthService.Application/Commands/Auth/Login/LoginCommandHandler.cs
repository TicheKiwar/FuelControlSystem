using AuthService.AuthService.Application.Common.Interfaces;
using AuthService.AuthService.Application.Common.Exceptions;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.AuthService.Application.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null)
            {
                throw new NotFoundException("Usuario no encontrado");
            }

            var authResult = await _authService.Authenticate(user, request.Password);

            if (!authResult.Success)
            {
                throw new UnauthorizedException("Credenciales inválidas");
            }

            return new AuthResponse(
                authResult.Token,
                authResult.RefreshToken,
                authResult.Expiration
                );
        }
    }
}
