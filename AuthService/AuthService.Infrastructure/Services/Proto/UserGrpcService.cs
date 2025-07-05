using Grpc.Core;
using MyApp.Grpc;
using AuthService.AuthService.Domain.Entities;
using AuthService.AuthService.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace AuthService.AuthService.Infrastructure.Services.Proto
{
    public class UserGrpcService : UserService.UserServiceBase
    {
        private readonly IUserRepository _userRepository;

        // Constructor para inyectar el repositorio
        public UserGrpcService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Implementación del método GetUser
        public override async Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            // Obtén el usuario de la base de datos usando el ID del request
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                // Si no se encuentra el usuario, lanzar una excepción o devolver un error
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            // Mapea la entidad User a UserResponse
            var userResponse = new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Roles = { user.Roles.Select(role => role.ToString()) }
            };

            return userResponse;
        }
    }
}
