using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocilaMediaProject.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role1");

            migrationBuilder.AddColumn<string>(
                name: "RoleID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "user", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b421e928-0613-9ebd-a64c-f10b6a706e73", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddresID", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Mileage", "NormalizedEmail", "NormalizedUserName", "Pace", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "RoleID", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22e40406-8a9d-2d82-912c-5d6a640ee696", 0, null, "c0b7d088-b183-462b-b5e9-70bf00661a8d", "Karim.abdelhameed@gmail.com", false, false, null, null, null, null, null, "lol123*As", "+002114515", false, null, null, "35ec3200-e7fb-4038-821c-520fa48a05fa", false, "Karim_Abbas" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b421e928-0613-9ebd-a64c-f10b6a706e73", "22e40406-8a9d-2d82-912c-5d6a640ee696" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleID",
                table: "AspNetUsers",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleID",
                table: "AspNetUsers",
                column: "RoleID",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleID",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b421e928-0613-9ebd-a64c-f10b6a706e73", "22e40406-8a9d-2d82-912c-5d6a640ee696" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b421e928-0613-9ebd-a64c-f10b6a706e73");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "HR", "HR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "role1", "1", "Admin", "Admin" });
        }
    }
}
