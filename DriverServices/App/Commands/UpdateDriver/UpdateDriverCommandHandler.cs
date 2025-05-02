using DriverServices.Domain.Entities;
using DriverServices.Domain.Interfaces.Repositories;
using MediatR;

namespace DriverServices.App.Commands.UpdateDriver
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, UpdateDriverResponse>
    {
        private readonly IDriverRepository _repository;

        public UpdateDriverCommandHandler(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateDriverResponse> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var existingDriver = await _repository.GetByIdAsync(request.Id);

            if (existingDriver == null)
            {
                return new UpdateDriverResponse
                {
                    Success = false,
                    Message = "Driver not found."
                };
            }

            var updatedDriver = new Driver
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dni = request.Dni,
                Address = request.Address,
                Phone = request.Phone,
                IsAvailability = request.IsAvailability,
                IsActive = request.IsActive,
                License = request.License,
                MachineryType = request.MachineryType,
                HourlyRate = request.HourlyRate
            };

            await _repository.UpdateAsync(updatedDriver);

            return new UpdateDriverResponse
            {
                Success = true,
                Message = "Driver updated successfully."
            };
        }
    }
}
