using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class hopefulfix2 : Migration
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
                    { "228653b0-ca76-434b-9a11-977539a55be0", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "8ad25f28-f028-4c50-96a0-a0f6bf8c776d", null, "API_Admin", "API_ADMIN" },
                    { "ddef58d1-86ad-4b99-8906-2fe0fc9e34fe", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "228653b0-ca76-434b-9a11-977539a55be0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ad25f28-f028-4c50-96a0-a0f6bf8c776d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddef58d1-86ad-4b99-8906-2fe0fc9e34fe");

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
