using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedOrderModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01943585-0fc0-4bb6-9efd-eb2493e6ff1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29d77218-5cc7-4b16-bee4-30e471c7b257");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c71fe2c-9bc1-4979-b7e2-1a3909b4d521");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7e1bf9e7-4fa3-45dd-90f5-3e1a6ab4f571", null, "API_Admin", "API_ADMIN" },
                    { "daf30339-f51b-4a64-b7b2-6b43224d5297", null, "User", "USER" },
                    { "ddba3923-af40-4543-8bf8-9bee2f56b502", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e1bf9e7-4fa3-45dd-90f5-3e1a6ab4f571");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daf30339-f51b-4a64-b7b2-6b43224d5297");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddba3923-af40-4543-8bf8-9bee2f56b502");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01943585-0fc0-4bb6-9efd-eb2493e6ff1e", null, "User", "USER" },
                    { "29d77218-5cc7-4b16-bee4-30e471c7b257", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "9c71fe2c-9bc1-4979-b7e2-1a3909b4d521", null, "API_Admin", "API_ADMIN" }
                });
        }
    }
}
