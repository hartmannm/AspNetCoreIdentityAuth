using ANCIA.Authentication.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ANCIA.Authentication.Configuration
{
    public static class DatabaseConfig
    {
        private const string ConnectionStringName = "DatabaseConnection";

        public static IServiceCollection ConfigureDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = GetConnectionString(configuration);
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStringName);
        }
    }
}
