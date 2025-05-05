using DriverServices.App.Commands;
using DriverServices.App.Interfaces;
using MediatR;

namespace DriverServices.App.Behavior
{
    public class VerifyUserIdBehavior : IPipelineBehavior<CreateDriverRegistrationCommand, CreateDriverRegistrationResult>
    {
        private readonly IUserClient _userClient;

        public VerifyUserIdBehavior(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public async Task<CreateDriverRegistrationResult> Handle(CreateDriverRegistrationCommand request, RequestHandlerDelegate<CreateDriverRegistrationResult> next, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = await _userClient.GetUserAsync(request.UserId);

                if (userDto == null)
                {
                    // Si no se encuentra el usuario, retornamos un error
                    return new CreateDriverRegistrationResult
                    {
                        Success = false,
                        Message = "El UserId proporcionado no es válido."
                    };
                }

                // Si el UserId es válido, pasa al siguiente comportamiento
               
            }   catch (Exception ex)
            {
                return new CreateDriverRegistrationResult
                {
                    Success = false,
                    Message = "El usuario no fue encontrado."
                };
            }         // Verificar si el UserId es válido usando el cliente gRPC
            return await next();
        }
    }
}
