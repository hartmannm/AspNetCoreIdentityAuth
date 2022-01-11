using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public bool Active { get; private set; }

        public AppUser()
        {
            Active = true;
        }

        public AppUser(string email) : this()
        {
            Email = email;
            UserName = email;
            EmailConfirmed = true;
        }
    }
}
