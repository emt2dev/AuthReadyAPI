using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class revertinit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c3a2248-38a0-48d5-a8c8-6801b1dddae6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b937c3e-5894-48e7-a1a4-df20c23e0674");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de990989-4ec4-44db-b937-a288bb6e9361");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421b6e1c-07c4-47cb-9943-80f8539baa79", null, "API_Admin", "API_ADMIN" },
                    { "4978969d-2bd9-4116-9759-430d518c1d5c", null, "User", "USER" },
                    { "b4f265d6-b418-4b52-8323-477d1b004d13", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421b6e1c-07c4-47cb-9943-80f8539baa79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4978969d-2bd9-4116-9759-430d518c1d5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4f265d6-b418-4b52-8323-477d1b004d13");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8c3a2248-38a0-48d5-a8c8-6801b1dddae6", null, "API_Admin", "API_ADMIN" },
                    { "9b937c3e-5894-48e7-a1a4-df20c23e0674", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "de990989-4ec4-44db-b937-a288bb6e9361", null, "User", "USER" }
                });
        }
    }
}
