using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Application.Services;
using ANCIA.Authentication.Application.Token;
using ANCIA.Authentication.Domain.Token;
using ANCIA.Authentication.Domain.User;
using ANCIA.Authentication.Infra.API;
using ANCIA.Core.Messages.Mediator;
using ANCIA.Core.Notifications;
using MediatR;
using System.Reflection;

namespace ANCIA.Authentication.Configuration
{
    public static class ContainerConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSqlServerDatabase(configuration);
            services.ConfigureRedisDatabase(configuration);
            services.Configure<TokenRules>(configuration.GetSection("TokenRules"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, ApiUser>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();
            return services;
        }
    }
}
