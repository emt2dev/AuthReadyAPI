using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class v2_complete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_v2_Staffs_administratorOneId",
                table: "v2_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_v2_Staffs_administratorTwoId",
                table: "v2_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_v2_Staffs_ownerId",
                table: "v2_Companies");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Full__Product");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "v2_Customers");

            migrationBuilder.DropTable(
                name: "shoppingCarts");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_v2_Staffs",
                table: "v2_Staffs");

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

            migrationBuilder.RenameTable(
                name: "v2_Staffs",
                newName: "v2_Staff");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AspNetUsers",
                newName: "companyId");

            migrationBuilder.RenameColumn(
                name: "IsStaff",
                table: "AspNetUsers",
                newName: "giveDeveloperPrivledges");

            migrationBuilder.AlterColumn<long>(
                name: "default_price",
                table: "v2_Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "v2_Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "v2_Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "v2_ShoppingCartId",
                table: "v2_Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addressCity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addressPostal_code",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addressState",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addressStreet",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "addressSuite",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "coordinates",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "giveAdminPrivledges",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "latitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "longitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripeId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "companyId",
                table: "v2_Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_v2_Staff",
                table: "v2_Staff",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "v2_ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    submitted = table.Column<bool>(type: "bit", nullable: false),
                    abandoned = table.Column<bool>(type: "bit", nullable: false),
                    costInString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "v2_Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    delivery = table.Column<bool>(type: "bit", nullable: false),
                    deliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timeDelivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    pickedUpByCustomer = table.Column<bool>(type: "bit", nullable: false),
                    timePickedUpByCustomer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    orderCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_v2_Orders_v2_ShoppingCarts_cartId",
                        column: x => x.cartId,
                        principalTable: "v2_ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_v2_Orders_v2_Staff_DriverId",
                        column: x => x.DriverId,
                        principalTable: "v2_Staff",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_v2_Products_v2_ShoppingCartId",
                table: "v2_Products",
                column: "v2_ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Orders_cartId",
                table: "v2_Orders",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Orders_DriverId",
                table: "v2_Orders",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_AspNetUsers_administratorOneId",
                table: "v2_Companies",
                column: "administratorOneId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_AspNetUsers_administratorTwoId",
                table: "v2_Companies",
                column: "administratorTwoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_AspNetUsers_ownerId",
                table: "v2_Companies",
                column: "ownerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Products_v2_ShoppingCarts_v2_ShoppingCartId",
                table: "v2_Products",
                column: "v2_ShoppingCartId",
                principalTable: "v2_ShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_AspNetUsers_administratorOneId",
                table: "v2_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_AspNetUsers_administratorTwoId",
                table: "v2_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Companies_AspNetUsers_ownerId",
                table: "v2_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_v2_Products_v2_ShoppingCarts_v2_ShoppingCartId",
                table: "v2_Products");

            migrationBuilder.DropTable(
                name: "v2_Orders");

            migrationBuilder.DropTable(
                name: "v2_ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_v2_Products_v2_ShoppingCartId",
                table: "v2_Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_v2_Staff",
                table: "v2_Staff");

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

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "v2_Products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "v2_Products");

            migrationBuilder.DropColumn(
                name: "v2_ShoppingCartId",
                table: "v2_Products");

            migrationBuilder.DropColumn(
                name: "addressCity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "addressPostal_code",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "addressState",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "addressStreet",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "addressSuite",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "coordinates",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "giveAdminPrivledges",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "latitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "position",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "stripeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "v2_Staff");

            migrationBuilder.RenameTable(
                name: "v2_Staff",
                newName: "v2_Staffs");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUsers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "companyId",
                table: "AspNetUsers",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "giveDeveloperPrivledges",
                table: "AspNetUsers",
                newName: "IsStaff");

            migrationBuilder.AlterColumn<long>(
                name: "default_price",
                table: "v2_Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_v2_Staffs",
                table: "v2_Staffs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_admin_one = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_admin_two = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    abandoned = table.Column<bool>(type: "bit", nullable: false),
                    companyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    costInString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    submitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "v2_Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressPostal_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stripeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APIUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount_Rate = table.Column<int>(type: "int", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Total_Amount = table.Column<double>(type: "float", nullable: false),
                    Total_Discounted = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_APIUserId",
                        column: x => x.APIUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APIUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination_latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination_longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment_Amount = table.Column<double>(type: "float", nullable: false),
                    Payment_Complete = table.Column<bool>(type: "bit", nullable: false),
                    Time__Delivered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Time__Submitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time__Touched = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delivery = table.Column<bool>(type: "bit", nullable: false),
                    delivery_driver_latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delivery_driver_longitdte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delivery_driver_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_APIUserId",
                        column: x => x.APIUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId1 = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modifiers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_Current = table.Column<double>(type: "float", nullable: false),
                    Price_Normal = table.Column<double>(type: "float", nullable: false),
                    Price_Sale = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    count = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    productId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Full__Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modifiers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_Current = table.Column<double>(type: "float", nullable: false),
                    Price_Normal = table.Column<double>(type: "float", nullable: false),
                    Price_Sale = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Full__Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Full__Product_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
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
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_shoppingCartId",
                table: "CartItem",
                column: "shoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_APIUserId",
                table: "Carts",
                column: "APIUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CompanyId",
                table: "Carts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Full__Product_CartId",
                table: "Full__Product",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_APIUserId",
                table: "Orders",
                column: "APIUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId1",
                table: "Products",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_v2_Staffs_administratorOneId",
                table: "v2_Companies",
                column: "administratorOneId",
                principalTable: "v2_Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_v2_Staffs_administratorTwoId",
                table: "v2_Companies",
                column: "administratorTwoId",
                principalTable: "v2_Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_v2_Companies_v2_Staffs_ownerId",
                table: "v2_Companies",
                column: "ownerId",
                principalTable: "v2_Staffs",
                principalColumn: "Id");
        }
    }
}
