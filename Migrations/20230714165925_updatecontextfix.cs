using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatecontextfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27c2f20b-2be9-42ac-bdd2-74d19ad2133f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c75ce58-4588-42e8-a502-64a4869e2e32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "367ebcee-89ea-4c54-a8e0-46208d7da652");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e144ec-e86d-4cfb-af9c-4d0d14376281");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "27c2f20b-2be9-42ac-bdd2-74d19ad2133f", null, "Staff", "STAFF" },
                    { "2c75ce58-4588-42e8-a502-64a4869e2e32", null, "Customer", "CUSTOMER" },
                    { "367ebcee-89ea-4c54-a8e0-46208d7da652", null, "Owner", "OWNER" },
                    { "62e144ec-e86d-4cfb-af9c-4d0d14376281", null, "Developer", "DEVELOPER" }
                });
        }
    }
}
