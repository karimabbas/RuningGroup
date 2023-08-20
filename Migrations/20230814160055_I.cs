using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocilaMediaProject.Migrations
{
    /// <inheritdoc />
    public partial class I : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ss",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7bf9a5b6-a96c-42f9-8ec2-6492f733690a", "cdf7345c-2f86-49c1-b981-6ac81eced69a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ss",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "42aeade1-99d6-44a0-ab2b-ea4bfca3a714", "41538fde-a9e7-439d-970d-6d5e10205668" });
        }
    }
}
