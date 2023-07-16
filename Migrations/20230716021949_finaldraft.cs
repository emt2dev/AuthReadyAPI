using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class finaldraft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stripeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressSuite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressPostal_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    companyId = table.Column<int>(type: "int", nullable: true),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giveAdminPrivledges = table.Column<bool>(type: "bit", nullable: false),
                    giveDeveloperPrivledges = table.Column<bool>(type: "bit", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "v2_Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_v2_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_v2_Companies_AspNetUsers_administratorOneId",
                        column: x => x.administratorOneId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_v2_Companies_AspNetUsers_administratorTwoId",
                        column: x => x.administratorTwoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_v2_Companies_AspNetUsers_ownerId",
                        column: x => x.ownerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
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
                    orderCompleted = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    method = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "v2_Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    companyId = table.Column<int>(type: "int", nullable: false),
                    stripeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    default_price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    package_dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statement_descriptor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    unit_label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priceInString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    v2_Companyid = table.Column<int>(type: "int", nullable: true),
                    v2_ShoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v2_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_v2_Products_v2_Companies_v2_Companyid",
                        column: x => x.v2_Companyid,
                        principalTable: "v2_Companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_v2_Products_v2_ShoppingCarts_v2_ShoppingCartId",
                        column: x => x.v2_ShoppingCartId,
                        principalTable: "v2_ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c28211f-75ca-4539-88cb-13090af306a2", null, "Owner", "OWNER" },
                    { "43993574-3957-46ac-85b8-a43b19e77daa", null, "Staff", "STAFF" },
                    { "7350ab64-9ecf-4f73-b55d-b79c31e62570", null, "Customer", "CUSTOMER" },
                    { "ca7c72d4-df73-4b16-b3dd-3155ba1e280a", null, "Developer", "DEVELOPER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_v2_Orders_cartId",
                table: "v2_Orders",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Orders_DriverId",
                table: "v2_Orders",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Products_v2_Companyid",
                table: "v2_Products",
                column: "v2_Companyid");

            migrationBuilder.CreateIndex(
                name: "IX_v2_Products_v2_ShoppingCartId",
                table: "v2_Products",
                column: "v2_ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "v2_Orders");

            migrationBuilder.DropTable(
                name: "v2_Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "v2_Staff");

            migrationBuilder.DropTable(
                name: "v2_Companies");

            migrationBuilder.DropTable(
                name: "v2_ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
