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
    }
}
