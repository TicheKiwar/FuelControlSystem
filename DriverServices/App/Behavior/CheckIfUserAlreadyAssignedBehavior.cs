using DriverServices.App.Commands;
using DriverServices.Domain.Interfaces.Repositories;
using MediatR;

namespace DriverServices.App.Behavior
{
    public class CheckIfUserAlreadyAssignedBehavior : IPipelineBehavior<CreateDriverRegistrationCommand, CreateDriverRegistrationResult>
    {
        private readonly IDriverRepository _driverRepository;

        public CheckIfUserAlreadyAssignedBehavior(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<CreateDriverRegistrationResult> Handle(CreateDriverRegistrationCommand request, RequestHandlerDelegate<CreateDriverRegistrationResult> next, CancellationToken cancellationToken)
        {
            try
            {
                var existingDriver = await _driverRepository.GetByIdAsync(request.UserId);
            if (existingDriver != null)
            {
                return new CreateDriverRegistrationResult
                {
                    Success = false,
                    Message = "El UserId ya está asignado a otro conductor."
                };
            }
            }
            catch (Exception ex)
            {
                return new CreateDriverRegistrationResult
                {
                    Success = false,
                    Message = "El usuario no fue encontrado."
                };
            }         
                return await next();
        }
    }
}
