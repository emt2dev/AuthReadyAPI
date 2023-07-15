using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class possibleincludefix01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e0987a8-843b-4173-8089-d74b1497fcb5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c3b5cc1-daf6-4669-a183-eba1bfc8371e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1e0adb6-eef2-4d94-ba90-0dbe73c85283");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8b5ce89-a2fd-42a8-a9b8-40bc9d13b02a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00b4e127-1d44-4159-bf64-fcc632ec8a3f", null, "Staff", "STAFF" },
                    { "6bc8787c-3e6c-46a7-b756-fbae1e7e3409", null, "Owner", "OWNER" },
                    { "adc7b7f9-b6d7-47f0-946f-841a91ae346c", null, "Developer", "DEVELOPER" },
                    { "d4b8ec25-2e76-448a-baed-df68a4de78b6", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00b4e127-1d44-4159-bf64-fcc632ec8a3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bc8787c-3e6c-46a7-b756-fbae1e7e3409");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adc7b7f9-b6d7-47f0-946f-841a91ae346c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4b8ec25-2e76-448a-baed-df68a4de78b6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e0987a8-843b-4173-8089-d74b1497fcb5", null, "Customer", "CUSTOMER" },
                    { "4c3b5cc1-daf6-4669-a183-eba1bfc8371e", null, "Developer", "DEVELOPER" },
                    { "a1e0adb6-eef2-4d94-ba90-0dbe73c85283", null, "Owner", "OWNER" },
                    { "f8b5ce89-a2fd-42a8-a9b8-40bc9d13b02a", null, "Staff", "STAFF" }
                });
        }
    }
}
