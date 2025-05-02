using DriverServices.Domain.Entities;
using DriverServices.Domain.Interfaces.Repositories;
using MediatR;

namespace DriverServices.App.Commands
{
    public class CreateDriverRegistrationHandler : IRequestHandler<CreateDriverRegistrationCommand, CreateDriverRegistrationResult>
    {
        private readonly IDriverRepository _driverRepository;

        public CreateDriverRegistrationHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<CreateDriverRegistrationResult> Handle(CreateDriverRegistrationCommand request, CancellationToken cancellationToken)
        {
            var driver = new Driver
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Address = request.Address,
                Phone = request.Phone,
                IsActive = request.IsActive,
                License = request.License,
                MachineryType = request.MachineryType,
                HourlyRate = request.HourlyRate
            };


            await _driverRepository.AddAsync(driver);

            return new CreateDriverRegistrationResult {
                Id = driver.Id,
                Success = true,
                Message = "Driver registered successfully" 
            };
        }
    }
}
