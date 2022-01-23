using ANCIA.Authentication.Domain.Token;
using ANCIA.Authentication.Infra.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ANCIA.Authentication.Configuration
{
    public static class AuthenticationConfig
    {
        private const string TokenRulesSection = "TokenRules";

        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenRules = GetTokenRules(services, configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.RequireAuthenticatedSignIn = true;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenRules.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = tokenRules.Audience,
                    ValidIssuer = tokenRules.Issuer,
                };
            });
            return services;
        }

        public static IApplicationBuilder UseAuthenticationConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<RefreshTokenMiddleware>();
            return app;
        }

        private static TokenRules GetTokenRules(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection tokenConfigSection = configuration.GetSection(TokenRulesSection);
            services.Configure<TokenRules>(tokenConfigSection);
            return tokenConfigSection.Get<TokenRules>();
        }
    }
}
