using Common.Shared;
using DriverServices.Infrastructure;
using VehicleService.App.Commands.Create;
using VehicleService.App.Commands.Update;


var app = Config.CreateWebApplication(
    configureInfrastructure: (services, configuration) =>
    {
        services.AddInfrastructure(configuration);
    },
        applicationAssemblies: new[] { 
            typeof(CreateVehicleCommandHandler).Assembly,
            typeof(UpdateVehicleCommandHandler).Assembly
        }
    );

Config.RunWebAplication(app);