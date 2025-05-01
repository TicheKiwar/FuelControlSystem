using FluentValidation;

namespace AuthService.AuthService.Api

{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar todos los servicios de la capa de aplicación
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));

            // Registrar validadores FluentValidation
            services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);

            // Otros servicios de aplicación
            // services.AddScoped<IMyApplicationService, MyApplicationService>();

            return services;
        }
    }
}
