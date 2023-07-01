using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixeddtoquestionmark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98161368-528a-4c53-9026-fae6bbd7c90c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcdde897-ddd4-49c4-bc56-8829ee1e663e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f48badf0-5760-4a98-b5c7-4cc2dcd92522");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "449e6715-3723-47f9-a895-c0cec99925b2", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "9adb03db-6959-4aac-bb33-9f6098ae8235", null, "User", "USER" },
                    { "c806773f-beda-4e7d-9241-91b25a8e5370", null, "API_Admin", "API_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "449e6715-3723-47f9-a895-c0cec99925b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9adb03db-6959-4aac-bb33-9f6098ae8235");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c806773f-beda-4e7d-9241-91b25a8e5370");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "98161368-528a-4c53-9026-fae6bbd7c90c", null, "API_Admin", "API_ADMIN" },
                    { "bcdde897-ddd4-49c4-bc56-8829ee1e663e", null, "User", "USER" },
                    { "f48badf0-5760-4a98-b5c7-4cc2dcd92522", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }
    }
}
