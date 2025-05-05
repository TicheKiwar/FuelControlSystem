using Common.Shared;
using DriverServices.Infrastructure;
using VehicleService.App.Commands.Create;
using VehicleService.App.Commands.Update;
using VehicleService.Infrastructure.Proto;


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
app.MapGrpcService<VehicleGrpcService>();

Config.RunWebAplication(app);