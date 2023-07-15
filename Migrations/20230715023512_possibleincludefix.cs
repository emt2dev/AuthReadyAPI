using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class possibleincludefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40a08a88-5bcc-49ab-a1c2-c4702966b05b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bf385aa-4c3d-4347-8694-dd284247abaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6198c92b-692f-45f4-8b2d-0709b599f42a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af841408-0723-4125-8ae9-ccb58911bc93");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "40a08a88-5bcc-49ab-a1c2-c4702966b05b", null, "Developer", "DEVELOPER" },
                    { "4bf385aa-4c3d-4347-8694-dd284247abaf", null, "Staff", "STAFF" },
                    { "6198c92b-692f-45f4-8b2d-0709b599f42a", null, "Owner", "OWNER" },
                    { "af841408-0723-4125-8ae9-ccb58911bc93", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
