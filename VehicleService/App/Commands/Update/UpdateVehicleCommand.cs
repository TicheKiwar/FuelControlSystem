using Common.Shared.Enums;
using Common.Shared.message;
using MediatR;
using VehicleService.Domain.Entities;

namespace VehicleService.App.Commands.Update
{
        public class UpdateVehicleCommand : IRequest<MessageResponse<Vehicle>>
        {
            public string Id { get; set; }
            public string PlateNumber { get; set; }
            public VehicleType Type { get; set; }
            public FuelType FuelType { get; set; }
            public double FuelEfficiency { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }

            public UpdateVehicleCommand(
                string id,
                string plateNumber,
                VehicleType type,
                FuelType fuelType,
                double fuelEfficiency,
                string brand,
                string model)
            {
                Id = id;
                PlateNumber = plateNumber;
                Type = type;
                FuelType = fuelType;
                FuelEfficiency = fuelEfficiency;
                Brand = brand;
                Model = model;
            }
        }
    }

