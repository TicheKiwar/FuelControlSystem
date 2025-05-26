using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;

namespace VehicleService.App.Commands.Create
{
    public class CreateVehicleCommand : IRequest<MessageResponse<Vehicle>>
    {
        public string PlateNumber { get; set; }
        public VehicleType Type { get; set; }
        public FuelType FuelType { get; set; }
        public double FuelEfficiency { get; set; }
        public double AverageFuelEfficiency { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsUnderMaintenance { get; set; }

        public CreateVehicleCommand(
            string plateNumber,
            VehicleType type,
            FuelType fuelType,
            double fuelEfficiency,
            double averageFuelEfficiency,
            string brand,
            string model,
            DateTime acquisitionDate,
            decimal purchasePrice,
            string documentNumber,
            bool isUnderMaintenance)
        {
            PlateNumber = plateNumber;
            Type = type;
            FuelType = fuelType;
            FuelEfficiency = fuelEfficiency;
            AverageFuelEfficiency = averageFuelEfficiency;
            Brand = brand;
            Model = model;
            AcquisitionDate = acquisitionDate;
            PurchasePrice = purchasePrice;
            DocumentNumber = documentNumber;
            IsUnderMaintenance = isUnderMaintenance;
        }
    }
}