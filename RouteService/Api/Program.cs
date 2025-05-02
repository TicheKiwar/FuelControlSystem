using Common.Shared;
using DriverServices.Infrastructure;
using RouteService.App.Commands.Routes;
using RouteService.App.Commands.Trips;

var app = Config.CreateWebApplication(
    configureInfrastructure: (services, configuration) =>
    {
        services.AddInfrastructure(configuration);
    },
    applicationAssemblies: new[] {
        typeof(CreateTripCommandHandler).Assembly,        typeof(CreateRouteCommandHandler).Assembly,
        typeof(UpdateTripCommandHandler).Assembly,        typeof(UpdateRouteCommandHandler).Assembly,
    }
);

Config.RunWebAplication(app);
