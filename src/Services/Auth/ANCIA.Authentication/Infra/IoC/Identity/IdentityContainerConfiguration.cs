using ANCIA.Authentication.Domain.Models;
using ANCIA.Authentication.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Infra.IoC.Identity
{
    public static class IdentityContainerConfiguration
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddErrorDescriber<IdentityPortugueseTranslation>();
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
