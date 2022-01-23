using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANCIA.Authentication.Migrations
{
    public partial class UserRolesInitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea", "95e5e4eb-76f8-489d-b879-33a1d5ef91f6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f191454a-ac50-4691-ab22-4abe17676070", "4cab1433-381f-4d46-89db-20850e272936", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5d99c8d7-1930-4660-8505-a84727bee152", 0, true, "a6e78295-b748-43f5-a7c8-6118c0b85452", "admin@admin.com", true, true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEMAEqw3Dw26kj4mcL/WV5Q4eYqJN5Q32fF83Q23eyWvQMDezoxtmwVdsCj4/6hPnrA==", null, false, "64OMQWV4UDW7WHJWOTW76BIRNT4XVGNW", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea", "5d99c8d7-1930-4660-8505-a84727bee152" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f191454a-ac50-4691-ab22-4abe17676070");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea", "5d99c8d7-1930-4660-8505-a84727bee152" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13da3007-dbaa-4b48-aaaf-83ac3dd5ffea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5d99c8d7-1930-4660-8505-a84727bee152");
        }
    }
}
