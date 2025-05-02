using Common.Shared;
using DriverServices.App.Commands;
using DriverServices.App.Commands.UpdateDriver;
using DriverServices.Infrastructure;


var app = Config.CreateWebApplication(
    configureInfrastructure: (services, configuration) =>
    {
        services.AddInfrastructure(configuration);
    },
    applicationAssemblies: new[] { 
        typeof(CreateDriverRegistrationHandler).Assembly,
        typeof(UpdateDriverCommandHandler).Assembly,
    }
);

Config.RunWebAplication(app);
