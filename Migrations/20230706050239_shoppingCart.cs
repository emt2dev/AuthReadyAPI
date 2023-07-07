using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class shoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6b3a915-3ebc-44d4-acd5-08537e2944dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8e33eaa-5f05-4efd-8f1c-df8ac98f6777");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc532878-d4d3-485c-a92b-641cea3763a7");

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    submitted = table.Column<bool>(type: "bit", nullable: false),
                    abandoned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_shoppingCarts_shoppingCartId",
                        column: x => x.shoppingCartId,
                        principalTable: "shoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bc39908-0c50-48dd-a163-86d5b6319916", null, "API_Admin", "API_ADMIN" },
                    { "466cce3c-3d5a-433f-ac0c-3726aece746f", null, "User", "USER" },
                    { "6ce41915-202d-4329-b862-a6f0829484aa", null, "Company_Admin", "COMPANY_ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_shoppingCartId",
                table: "CartItem",
                column: "shoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bc39908-0c50-48dd-a163-86d5b6319916");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "466cce3c-3d5a-433f-ac0c-3726aece746f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ce41915-202d-4329-b862-a6f0829484aa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b6b3a915-3ebc-44d4-acd5-08537e2944dd", null, "User", "USER" },
                    { "d8e33eaa-5f05-4efd-8f1c-df8ac98f6777", null, "API_Admin", "API_ADMIN" },
                    { "fc532878-d4d3-485c-a92b-641cea3763a7", null, "Company_Admin", "COMPANY_ADMIN" }
                });
        }
    }
}
