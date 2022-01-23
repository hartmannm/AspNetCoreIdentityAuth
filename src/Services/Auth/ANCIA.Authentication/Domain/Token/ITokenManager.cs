using ANCIA.Authentication.Domain.Models;
using System.Security.Claims;

namespace ANCIA.Authentication.Application.AuthToken
{
    public interface ITokenManager
    {
        Task<string> CreateToken(AppUser user);

        string CreateToken(IEnumerable<Claim> claims);

        string CreateRefreshToken();

        Task SaveRefreshToken(string userId, string token);

        Task<bool> IsRefreshTokenValid(Guid userid, string refreshToken);
    }
}
