using System.Security.Claims;

namespace ANCIA.Authentication.Domain.User
{
    public interface IUser
    {
        string Name { get; }

        public Guid GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
