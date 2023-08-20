using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocilaMediaProject.Migrations
{
    /// <inheritdoc />
    public partial class appuserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Clubs",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AppUserId",
                table: "Clubs",
                newName: "IX_Clubs_AppUserID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e4adf9f7-68d3-4af6-af40-40264b8c5316", "b0bc20ca-969a-4c9e-8add-cb1b16f50bb6" });

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserID",
                table: "Clubs",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserID",
                table: "Clubs");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Clubs",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_AppUserID",
                table: "Clubs",
                newName: "IX_Clubs_AppUserId");

            migrationBuilder.AddColumn<string>(
                name: "RoleID",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "RoleID", "SecurityStamp" },
                values: new object[] { "c0b7d088-b183-462b-b5e9-70bf00661a8d", null, "35ec3200-e7fb-4038-821c-520fa48a05fa" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
