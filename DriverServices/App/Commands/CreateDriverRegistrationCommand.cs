using Common.Shared.Enums;
using MediatR;

namespace DriverServices.App.Commands
{
    public class CreateDriverRegistrationCommand : IRequest<CreateDriverRegistrationResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsAvailability { get; set; }
        public bool IsActive { get; set; }
        public LicenseType License { get; set; }
        public VehicleType MachineryType { get; set; }

        public CreateDriverRegistrationCommand(string firstName, string lastName, string dni, string address, string phone, bool isAvailability, bool isActive, LicenseType license, VehicleType machineryType)
        {
            FirstName = firstName;
            LastName = lastName;
            Dni = dni;
            Address = address;
            Phone = phone;
            IsAvailability = isAvailability;
            IsActive = isActive;
            License = license;
            MachineryType = machineryType;
        }
    }
}
