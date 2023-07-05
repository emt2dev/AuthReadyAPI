using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class addQuantityToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Quanity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Quanity",
                table: "Products");

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
    }
}
