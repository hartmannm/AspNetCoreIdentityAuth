using ANCIA.Authentication.Domain.Models;
using ANCIA.Authentication.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Infra.IoC.Identity
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            })
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddErrorDescriber<IdentityPortugueseTranslation>()
                .AddDefaultTokenProviders();
            ConfigurePassword(services);
            return services;
        }

        private static void ConfigurePassword(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            });
        }
    }
}
