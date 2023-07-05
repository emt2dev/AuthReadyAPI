using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixsomebsquestionmark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "35e044ef-a918-4c78-b306-2ee7f0a7788f", null, "User", "USER" },
                    { "8685b498-cc3d-4519-b1eb-31b74554d022", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "eb37e9da-5c3d-49c7-bbef-94e9e797ee4b", null, "API_Admin", "API_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35e044ef-a918-4c78-b306-2ee7f0a7788f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8685b498-cc3d-4519-b1eb-31b74554d022");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb37e9da-5c3d-49c7-bbef-94e9e797ee4b");

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
    }
}
