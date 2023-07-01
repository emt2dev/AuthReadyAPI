using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class sqlcontainerupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e860b0c-aef9-4482-81f1-309d24583fa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa9e7d11-eb82-4c8e-bc37-3dec94db9a3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a42998-c2d0-440f-a7ab-8722babdb91d");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7e860b0c-aef9-4482-81f1-309d24583fa1", null, "User", "USER" },
                    { "aa9e7d11-eb82-4c8e-bc37-3dec94db9a3b", null, "API_Admin", "API_ADMIN" },
                    { "f4a42998-c2d0-440f-a7ab-8722babdb91d", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }
    }
}
