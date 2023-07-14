using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46888851-1b1b-418c-94e1-5948e9f68d6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90e79bcc-2709-450b-bff3-7e89cffce9d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52a185a-c8a0-46f4-9cb8-51f41d050274");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1f7c4fd-1328-47ea-a696-2238668cf519");

            migrationBuilder.AddColumn<string>(
                name: "eta",
                table: "v2_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "method",
                table: "v2_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "v2_Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "eta",
                table: "v2_Orders");

            migrationBuilder.DropColumn(
                name: "method",
                table: "v2_Orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "v2_Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46888851-1b1b-418c-94e1-5948e9f68d6e", null, "Developer", "DEVELOPER" },
                    { "90e79bcc-2709-450b-bff3-7e89cffce9d4", null, "Customer", "CUSTOMER" },
                    { "a52a185a-c8a0-46f4-9cb8-51f41d050274", null, "Staff", "STAFF" },
                    { "d1f7c4fd-1328-47ea-a696-2238668cf519", null, "Owner", "OWNER" }
                });
        }
    }
}
