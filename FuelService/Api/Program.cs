using Common.Shared;
using DriverServices.Infrastructure;

var app = Config.CreateWebApplication(
    configureInfrastructure: (services, configuration) =>
    {
        services.AddInfrastructure(configuration);
    }
);

Config.RunWebAplication(app);
