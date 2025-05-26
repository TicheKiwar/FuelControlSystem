using Common.Shared;
using DriverServices.App.Commands;
using DriverServices.App.Commands.UpdateDriver;
using DriverServices.App.Interfaces;
using DriverServices.Infrastructure;
using DriverServices.Infrastructure.Services.Client;
using DriverServices.Infrastructure.Services.Proto;


var app = Config.CreateWebApplication(
    configureInfrastructure: (services, configuration) =>
    {
        services.AddInfrastructure(configuration);
        services.AddSingleton<IUserClient, UserClient>();
    },

    applicationAssemblies: new[] { 
        typeof(CreateDriverRegistrationHandler).Assembly,
        typeof(UpdateDriverCommandHandler).Assembly,
    }        
);
app.MapGrpcService<DriverGrpcService>();
Config.RunWebAplication(app);
