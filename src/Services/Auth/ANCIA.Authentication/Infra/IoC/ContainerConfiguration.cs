using ANCIA.Authentication.Infra.IoC.Data;

namespace ANCIA.Authentication.Infra.IoC
{
    public static class ContainerConfiguration
    {
        public static IServiceCollection ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabaseDependencies(configuration);
            services.ConfigureIdentity(configuration);
            return services;
        }
    }
}
