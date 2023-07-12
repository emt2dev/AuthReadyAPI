using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2modelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "v2_Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    stripeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressPostal_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "v2_Staffs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giveAdminPrivledges = table.Column<bool>(type: "bit", nullable: false),
                    giveDeveloperPrivledges = table.Column<bool>(type: "bit", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "v2_Companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressSuite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressPostal_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    smallTagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menuDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    headerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aboutUsImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locationImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logoImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    miscImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    administratorOneId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    administratorTwoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_v2_Companies_v2_Staffs_administratorOneId",
                        column: x => x.administratorOneId,
                        principalTable: "v2_Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_v2_Companies_v2_Staffs_administratorTwoId",
                        column: x => x.administratorTwoId,
                        principalTable: "v2_Staffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_v2_Companies_v2_Staffs_ownerId",
                        column: x => x.ownerId,
                        principalTable: "v2_Staffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "v2_Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stripeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    default_price = table.Column<long>(type: "bigint", nullable: true),
                    package_dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statement_descriptor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    unit_label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priceInString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    v2_Companyid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_v2_Products_v2_Companies_v2_Companyid",
                        column: x => x.v2_Companyid,
                        principalTable: "v2_Companies",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16b3ce20-e3a5-4487-9eaa-4a6d30a7ea1b", null, "User", "USER" },
                    { "730ee3e5-70b1-49b9-a476-ff24133adbfe", null, "API_Admin", "API_ADMIN" },
                    { "fb0d1b23-5098-4d38-ac92-a5d90df8492b", null, "Company_Admin", "COMPANY_ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_v2_Companies_administratorOneId",
                table: "v2_Companies",
                column: "administratorOneId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Companies_administratorTwoId",
                table: "v2_Companies",
                column: "administratorTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Companies_ownerId",
                table: "v2_Companies",
                column: "ownerId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Products_v2_Companyid",
                table: "v2_Products",
                column: "v2_Companyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "v2_Customers");

            migrationBuilder.DropTable(
                name: "v2_Products");

            migrationBuilder.DropTable(
                name: "v2_Companies");

            migrationBuilder.DropTable(
                name: "v2_Staffs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16b3ce20-e3a5-4487-9eaa-4a6d30a7ea1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "730ee3e5-70b1-49b9-a476-ff24133adbfe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb0d1b23-5098-4d38-ac92-a5d90df8492b");

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
    }
}
