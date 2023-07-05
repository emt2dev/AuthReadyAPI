using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class newdtochanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "6e9d2850-cee6-4724-8ef3-04a5197b2850", null, "API_Admin", "API_ADMIN" },
                    { "7f0bc85a-bdd4-4b0c-9fbe-deb7bce8e42b", null, "User", "USER" },
                    { "faee0767-0a60-4c53-a3c3-764e5a777f50", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e9d2850-cee6-4724-8ef3-04a5197b2850");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f0bc85a-bdd4-4b0c-9fbe-deb7bce8e42b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faee0767-0a60-4c53-a3c3-764e5a777f50");

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
    }
}
