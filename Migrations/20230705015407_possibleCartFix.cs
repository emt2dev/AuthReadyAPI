using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class possibleCartFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a36330a-8b4b-43eb-985c-27522dccad83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c697457-cad0-4aae-bf6d-688d63084849");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9a5c567-d05f-4007-b71c-995faade7ced");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "84a6f9c7-052d-41c6-928b-110650a8551b", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "c00adac9-3e1d-4fc3-8ef2-0d5e1aa679e6", null, "API_Admin", "API_ADMIN" },
                    { "f26ffd06-5079-43af-8668-5b60277841cc", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a6f9c7-052d-41c6-928b-110650a8551b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c00adac9-3e1d-4fc3-8ef2-0d5e1aa679e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f26ffd06-5079-43af-8668-5b60277841cc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a36330a-8b4b-43eb-985c-27522dccad83", null, "User", "USER" },
                    { "8c697457-cad0-4aae-bf6d-688d63084849", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "b9a5c567-d05f-4007-b71c-995faade7ced", null, "API_Admin", "API_ADMIN" }
                });
        }
    }
}
