using Microsoft.AspNetCore.Identity;

namespace ANCIA.Authentication.Domain.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole(string name)
        {
            Name = name;
        }
    }
}
