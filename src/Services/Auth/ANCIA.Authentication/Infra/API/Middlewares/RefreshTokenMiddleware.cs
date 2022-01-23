using ANCIA.Authentication.Application.AuthToken;
using ANCIA.Authentication.Domain.Token;
using ANCIA.Authentication.Domain.User;
using Microsoft.Extensions.Options;

namespace ANCIA.Authentication.Infra.API.Middlewares
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUser user, IOptions<TokenRules> tokenRulesConfig, ITokenManager tokenManager)
        {
            var tokenRules = tokenRulesConfig.Value;
            string refreshToken = context.Request.Headers[tokenRules.RefreshTokenHeader];

            if (user.IsAuthenticated() && refreshToken is not null)
                await RefreshAuthToken(refreshToken, context, user, tokenRules, tokenManager);

            await _next(context);
        }

        private async Task RefreshAuthToken(string refreshToken, HttpContext context, IUser user, TokenRules tokenRules, ITokenManager tokenManager)
        {
            var userId = user.GetUserId();
            if (await tokenManager.IsRefreshTokenValid(userId, refreshToken))
            {
                context.Response.OnStarting(async state =>
                {
                    var httpContext = (HttpContext)state;
                    try
                    {
                        var newToken = tokenManager.CreateToken(user.GetClaimsIdentity());
                        var newRefreshToken = tokenManager.CreateRefreshToken();
                        await tokenManager.SaveRefreshToken(userId.ToString(), newRefreshToken);

                        httpContext.Response.Headers.Add(tokenRules.NewAuthTokenHeader, newToken);
                        httpContext.Response.Headers.Add(tokenRules.NewRefreshTokenHeader, newRefreshToken);
                    }
                    catch (Exception)
                    {
                        Console.Error.WriteLine($"Create new token error. User: {userId}");
                    }
                }, context);
            }
        }
    }
}
