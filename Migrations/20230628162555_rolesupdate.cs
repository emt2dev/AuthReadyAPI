using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class rolesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46c725ac-151b-44fc-8a5e-c63442e81779", null, "User", "USER" },
                    { "6b9f6cee-905c-4488-8f34-84c4e1cf87f8", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "f2f02324-e530-4036-bd41-f423b80781ac", null, "API_Admin", "API_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46c725ac-151b-44fc-8a5e-c63442e81779");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b9f6cee-905c-4488-8f34-84c4e1cf87f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2f02324-e530-4036-bd41-f423b80781ac");
        }
    }
}
