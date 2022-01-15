using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Application.Token;
using ANCIA.Core.Messages.Mediator;
using ANCIA.Core.Notifications;
using MediatR;
using System.Reflection;

namespace ANCIA.Authentication.Infra.IoC
{
    public static class ServicesContainerConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ITokenManager, TokenManager>();
            return services;
        }
    }
}
