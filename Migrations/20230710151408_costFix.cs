using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class costFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "costInString",
                table: "shoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21f2ce8e-21ad-4f71-8316-3432e59c58f8", null, "User", "USER" },
                    { "2f703993-6d59-460d-8808-c72d700d17f2", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "d5f5e2d4-88aa-4f77-bf32-e580d7f3957d", null, "API_Admin", "API_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21f2ce8e-21ad-4f71-8316-3432e59c58f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f703993-6d59-460d-8808-c72d700d17f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5f5e2d4-88aa-4f77-bf32-e580d7f3957d");

            migrationBuilder.DropColumn(
                name: "costInString",
                table: "shoppingCarts");

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
    }
}
