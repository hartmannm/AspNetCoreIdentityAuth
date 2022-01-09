using ANCIA.Authentication.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Infra.IoC.Data
{
    public static class IdentityContainerConfiguration
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>();
            return services;
        }
    }
}
