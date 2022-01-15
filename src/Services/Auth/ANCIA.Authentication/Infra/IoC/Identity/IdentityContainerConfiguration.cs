using ANCIA.Authentication.Domain.Models;
using ANCIA.Authentication.Domain.Token;
using ANCIA.Authentication.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ANCIA.Authentication.Infra.IoC.Identity
{
    public static class IdentityContainerConfiguration
    {
        private const string TokenRulesSection = "TokenRules";

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
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
            ConfigureToken(services, configuration);
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

        private static void ConfigureToken(IServiceCollection services, IConfiguration configuration)
        {
            var tokenRules = GetTokenRules(services, configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenRules.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = tokenRules.Audience,
                    ValidIssuer = tokenRules.Issuer
                };
            });
        }

        private static TokenRules GetTokenRules(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection tokenConfigSection = configuration.GetSection(TokenRulesSection);
            services.Configure<TokenRules>(tokenConfigSection);
            return tokenConfigSection.Get<TokenRules>();
        }
    }
}
