
using System.Reflection;
using FluentValidation;


namespace Common.Shared.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(cfg =>
            {
                foreach (var assembly in assemblies)
                {
                    cfg.RegisterServicesFromAssembly(assembly);
                }
            });

            foreach (var assembly in assemblies)
            {
                services.AddValidatorsFromAssembly(assembly);
            }

            return services;
        }
    }
}
