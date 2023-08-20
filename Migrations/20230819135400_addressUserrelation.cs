using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocilaMediaProject.Migrations
{
    /// <inheritdoc />
    public partial class addressUserrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6cbe49a5-984a-45dd-abf9-f744d452d64c", "a0dbd288-46ba-4d08-baf3-2e4ed7322e6d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22e40406-8a9d-2d82-912c-5d6a640ee696",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e4adf9f7-68d3-4af6-af40-40264b8c5316", "b0bc20ca-969a-4c9e-8add-cb1b16f50bb6" });
        }
    }
}
