using ANCIA.Authentication.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ANCIA.Authentication.Configuration
{
    public static class DatabaseConfig
    {
        private const string SqlServerConnection = "DatabaseConnection";
        private const string RedisConnection = "RedisConnection";

        public static IServiceCollection ConfigureSqlServerDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = GetConnectionString(configuration);
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection ConfigureRedisDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString(RedisConnection);
                options.InstanceName = "IdentityAuth-";
            });
            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString(SqlServerConnection);
        }
    }
}
