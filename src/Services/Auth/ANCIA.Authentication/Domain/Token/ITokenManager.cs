using ANCIA.Authentication.Domain.Models;

namespace ANCIA.Authentication.Application.AuthToken
{
    public interface ITokenManager
    {
        Task<string> CreateToken(AppUser user);
    }
}
