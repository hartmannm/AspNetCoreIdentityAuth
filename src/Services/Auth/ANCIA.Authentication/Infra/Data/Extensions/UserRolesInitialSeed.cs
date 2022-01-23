using ANCIA.Authentication.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ANCIA.Authentication.Infra.Data.Extensions
{
    public static class UserRolesInitialSeed
    {
        public static void SeedInitialUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRoles();
            modelBuilder.SeedAdminUser();
            modelBuilder.SeedUserRolerelation();
        }

        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "95e5e4eb-76f8-489d-b879-33a1d5ef91f6"
                },
                new IdentityRole
                {
                    Id = "f191454a-ac50-4691-ab22-4abe17676070",
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    ConcurrencyStamp = "4cab1433-381f-4d46-89db-20850e272936"
                }
            );
        }

        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            var user = new AppUser("admin@admin.com")
            {
                Id = "5d99c8d7-1930-4660-8505-a84727bee152",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEMAEqw3Dw26kj4mcL/WV5Q4eYqJN5Q32fF83Q23eyWvQMDezoxtmwVdsCj4/6hPnrA==",
                SecurityStamp = "64OMQWV4UDW7WHJWOTW76BIRNT4XVGNW",
                ConcurrencyStamp = "a6e78295-b748-43f5-a7c8-6118c0b85452",
                LockoutEnabled = true
            };
            modelBuilder.Entity<AppUser>().HasData(user);
        }

        public static void SeedUserRolerelation(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea",
                    UserId = "5d99c8d7-1930-4660-8505-a84727bee152"
                }
            );
        }
    }
}
