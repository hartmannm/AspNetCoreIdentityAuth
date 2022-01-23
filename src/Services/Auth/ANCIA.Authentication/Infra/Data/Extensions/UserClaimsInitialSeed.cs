using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ANCIA.Authentication.Infra.Data.Extensions
{
    public static class UserClaimsInitialSeed
    {
        public static void SeedInitialClaims(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "5d99c8d7-1930-4660-8505-a84727bee152",
                    ClaimType = "User",
                    ClaimValue = "Update",
                },
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    UserId = "5d99c8d7-1930-4660-8505-a84727bee152",
                    ClaimType = "User",
                    ClaimValue = "Read",
                },
                new IdentityUserClaim<string>
                {
                    Id = 3,
                    UserId = "5d99c8d7-1930-4660-8505-a84727bee152",
                    ClaimType = "User",
                    ClaimValue = "Delete",
                }
            );
        }
    }
}
