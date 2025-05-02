using Common.Shared.Enums;
using MediatR;

namespace DriverServices.App.Commands.UpdateDriver
{
    public class UpdateDriverCommand : IRequest<UpdateDriverResponse>
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Dni { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public bool IsAvailability { get; set; }
        public bool IsActive { get; set; }
        public LicenseType License { get; set; } = default!;
        public VehicleType MachineryType { get; set; }

        public decimal HourlyRate { get; set; }
    }
}
