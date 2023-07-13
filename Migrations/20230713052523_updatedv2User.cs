using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedv2User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e2aeb26-e7a6-45b8-af4e-52cb55c58b3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60d22bb7-45d2-4b3f-9e95-95aa4f2bd898");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e95bdaa-5412-4052-8251-2b9782e83238");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92c3f372-7968-4bbe-9b61-9104f4dfb579");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e2aeb26-e7a6-45b8-af4e-52cb55c58b3e", null, "Staff", "STAFF" },
                    { "60d22bb7-45d2-4b3f-9e95-95aa4f2bd898", null, "Developer", "DEVELOPER" },
                    { "6e95bdaa-5412-4052-8251-2b9782e83238", null, "Customer", "CUSTOMER" },
                    { "92c3f372-7968-4bbe-9b61-9104f4dfb579", null, "Owner", "OWNER" }
                });
        }
    }
}
