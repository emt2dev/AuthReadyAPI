using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LifetimeSpent = table.Column<double>(type: "float", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
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
                name: "AuctionProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasBeenPurchased = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ShippingCost = table.Column<double>(type: "float", nullable: false),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBidAmount = table.Column<double>(type: "float", nullable: false),
                    CurrentBidderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingBidAmount = table.Column<double>(type: "float", nullable: false),
                    AutoSellBidAmount = table.Column<double>(type: "float", nullable: false),
                    AcceptAutoSell = table.Column<bool>(type: "bit", nullable: false),
                    AuctionEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointOfContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfTransaction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaleGross = table.Column<double>(type: "float", nullable: false),
                    CompanyNet = table.Column<double>(type: "float", nullable: false),
                    AuthReadyNet = table.Column<double>(type: "float", nullable: false),
                    DepositedToCompany = table.Column<bool>(type: "bit", nullable: false),
                    DepositTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false),
                    CouponApplied = table.Column<bool>(type: "bit", nullable: false),
                    PriceBeforeCoupon = table.Column<double>(type: "float", nullable: false),
                    PriceAfterCoupon = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CouponCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodDeliveryClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSubmitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeTouched = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeDelivered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLongitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverLatitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationLatitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationLongitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDeliveryClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodProductClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedByKitchen = table.Column<bool>(type: "bit", nullable: false),
                    GivenToCustomer = table.Column<bool>(type: "bit", nullable: false),
                    Refunded = table.Column<bool>(type: "bit", nullable: false),
                    TotalSpent = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FoodCartClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointOfContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreparedCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparedCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatesMade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false),
                    CouponApplied = table.Column<bool>(type: "bit", nullable: false),
                    PriceBeforeCoupon = table.Column<double>(type: "float", nullable: false),
                    PriceAfterCoupon = table.Column<double>(type: "float", nullable: false),
                    CouponCodeId = table.Column<int>(type: "int", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false),
                    CouponApplied = table.Column<bool>(type: "bit", nullable: false),
                    PriceBeforeCoupon = table.Column<double>(type: "float", nullable: false),
                    PriceAfterCoupon = table.Column<double>(type: "float", nullable: false),
                    CouponCodeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SingleProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasBeenPurchased = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    ShippingCost = table.Column<double>(type: "float", nullable: false),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Delivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProducts", x => x.Id);
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
                name: "AuctionCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false),
                    Expiration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionCarts_AuctionProducts_ItemId",
                        column: x => x.ItemId,
                        principalTable: "AuctionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuctionProductId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AuctionProductClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionProductImages_AuctionProducts_AuctionProductClassId",
                        column: x => x.AuctionProductClassId,
                        principalTable: "AuctionProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuctionProductId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Submitted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuctionProductClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_AuctionProducts_AuctionProductClassId",
                        column: x => x.AuctionProductClassId,
                        principalTable: "AuctionProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    PaymentCompleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentRefunded = table.Column<bool>(type: "bit", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    SingleProductCartClassId = table.Column<int>(type: "int", nullable: false),
                    AuctionProductCartClassId = table.Column<int>(type: "int", nullable: false),
                    ServicesCartClassId = table.Column<int>(type: "int", nullable: false),
                    FoodCartClassId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDelivery = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_FoodDeliveryClass_DeliveryInfoId",
                        column: x => x.DeliveryInfoId,
                        principalTable: "FoodDeliveryClass",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerShowed = table.Column<bool>(type: "bit", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ServicesCartClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_ServiceCarts_ServicesCartClassId",
                        column: x => x.ServicesCartClassId,
                        principalTable: "ServiceCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackagedWeight = table.Column<double>(type: "float", nullable: false),
                    PackagedDimensions = table.Column<double>(type: "float", nullable: false),
                    DigitalOnly = table.Column<bool>(type: "bit", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_ShoppingCartClassId",
                        column: x => x.ShoppingCartClassId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SingleProductCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Abandoned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProductCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleProductCarts_SingleProducts_ItemId",
                        column: x => x.ItemId,
                        principalTable: "SingleProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SingleProductId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    SingleProductClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleProductImages_SingleProducts_SingleProductClassId",
                        column: x => x.SingleProductClassId,
                        principalTable: "SingleProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DigitalOwnerShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DigitalOwnerUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadCount = table.Column<int>(type: "int", nullable: false),
                    ProductKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    OrderClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOwnerShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalOwnerShips_Orders_OrderClassId",
                        column: x => x.OrderClassId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShippingInfoClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    MaxWeight = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsFlatRate = table.Column<bool>(type: "bit", nullable: false),
                    IsWeighed = table.Column<bool>(type: "bit", nullable: false),
                    IsDigital = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryExpectation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    OrderClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInfoClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingInfoClass_Orders_OrderClassId",
                        column: x => x.OrderClassId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProducts_Appointments_AppointmentClassId",
                        column: x => x.AppointmentClassId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DiscountedPrice = table.Column<double>(type: "float", nullable: false),
                    UseDiscountedPrice = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Appointments_AppointmentClassId",
                        column: x => x.AppointmentClassId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductUpsells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostWithShipping = table.Column<double>(type: "float", nullable: false),
                    DigitalOnly = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuctionProductCartClassId = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartClassId = table.Column<int>(type: "int", nullable: true),
                    SingleProductCartClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUpsells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUpsells_AuctionCarts_AuctionProductCartClassId",
                        column: x => x.AuctionProductCartClassId,
                        principalTable: "AuctionCarts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductUpsells_ShoppingCarts_ShoppingCartClassId",
                        column: x => x.ShoppingCartClassId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductUpsells_SingleProductCarts_SingleProductCartClassId",
                        column: x => x.SingleProductCartClassId,
                        principalTable: "SingleProductCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductWithStyleClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ShippingInfoClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWithStyleClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWithStyleClass_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWithStyleClass_ShippingInfoClass_ShippingInfoClassId",
                        column: x => x.ShippingInfoClassId,
                        principalTable: "ShippingInfoClass",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<double>(type: "float", nullable: false),
                    CartCount = table.Column<int>(type: "int", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    GrossIncome = table.Column<double>(type: "float", nullable: false),
                    IsAvailableForOrder = table.Column<bool>(type: "bit", nullable: false),
                    IsLimitedTimeOnly = table.Column<bool>(type: "bit", nullable: false),
                    UnavailableOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsComingSoon = table.Column<bool>(type: "bit", nullable: false),
                    AvailableOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountedPrice = table.Column<double>(type: "float", nullable: false),
                    UseDiscountPrice = table.Column<bool>(type: "bit", nullable: false),
                    DigitalOnly = table.Column<bool>(type: "bit", nullable: false),
                    PackagedDimensions = table.Column<double>(type: "float", nullable: false),
                    PackagedWeight = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DigitalOwnerId = table.Column<int>(type: "int", nullable: false),
                    ProductWithStyleClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Styles_ProductWithStyleClass_ProductWithStyleClassId",
                        column: x => x.ProductWithStyleClassId,
                        principalTable: "ProductWithStyleClass",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "102829f7-8635-407c-ad4a-f3b78bed0100", null, "User", "USER" },
                    { "215338bc-c092-4687-9218-b65db1bfe1b2", null, "Admin", "ADMIN" },
                    { "d34e2822-6aa5-4548-ae25-6a1714c5f2b9", null, "Company", "COMPANY" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CompanyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Toy" },
                    { 2, 1, "Shirt" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Active", "Description", "Email", "MailingAddress", "Name", "PhoneNumber", "PointOfContactId", "ShippingAddress" },
                values: new object[] { 1, true, "Test company located in fake city.", "hello@testcompany.com", "123 Main Street, Any Town, EE, USA 11111", "Test Company", "1 1234567890", 0, "123 Main Street, Any Town, EE, USA 11111" });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "ProductId", "StyleId" },
                values: new object[,]
                {
                    { 1, "https://www.ebay.com/itm/325914851835?chn=ps&norover=1&mkevt=1&mkrid=711-117182-37290-0&mkcid=2&mkscid=101&itemid=325914851835&targetid=1531876732278&device=c&mktype=pla&googleloc=9008533&poi=&campaignid=19851828444&mkgroupid=145880009014&rlsatarget=pla-1531876732278&abcId=9307249&merchantid=101491518&gclid=Cj0KCQiAkeSsBhDUARIsAK3tiefJewYtYyA-3-PuCb51Ogy66tVAz1RAXPLNkQBGFKr6BWTdqSarwPEaArz0EALw_wcB", 1, 1 },
                    { 2, "https://i.ebayimg.com/images/g/rrIAAOSwITZdZAJM/s-l960.jpg", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CompanyId", "CreatedOn", "Description", "MainImageUrl", "Name", "Quantity", "TaxCode", "UpdatedBy", "UpdatedOn", "UpdatesMade", "ViewCount" },
                values: new object[] { 1, 1, 1, new DateTime(2024, 1, 8, 17, 28, 25, 504, DateTimeKind.Local).AddTicks(648), "Signature collection of barbie dolls.", "https://creations.mattel.com/cdn/shop/products/ors1kicv0rkdf2teoqaf.png?v=1684560830", "Barbie Signature Collection", 1, "tst_0009", "0", new DateTime(2024, 1, 8, 17, 28, 25, 504, DateTimeKind.Local).AddTicks(694), "System Generated", 0 });

            migrationBuilder.InsertData(
                table: "Styles",
                columns: new[] { "Id", "AvailableOn", "CartCount", "CompanyId", "CurrentPrice", "Description", "DigitalOnly", "DigitalOwnerId", "DiscountedPrice", "GrossIncome", "IsAvailableForOrder", "IsComingSoon", "IsLimitedTimeOnly", "Name", "OrderCount", "PackagedDimensions", "PackagedWeight", "ProductId", "ProductWithStyleClassId", "UnavailableOn", "UseDiscountPrice" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 9.9900000000000002, "Barbie Signature Rosa Parks Civil Rights Activist Inspiring Women Series", false, 0, 8.9900000000000002, 0.0, true, false, false, "Rosa Parks", 0, 99.0, 1.5, 1, null, new DateTime(2024, 1, 8, 17, 28, 25, 504, DateTimeKind.Local).AddTicks(792), false });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServicesCartClassId",
                table: "Appointments",
                column: "ServicesCartClassId");

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
                name: "IX_AuctionCarts_ItemId",
                table: "AuctionCarts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionProductImages_AuctionProductClassId",
                table: "AuctionProductImages",
                column: "AuctionProductClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AuctionProductClassId",
                table: "Bids",
                column: "AuctionProductClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartClassId",
                table: "CartItems",
                column: "ShoppingCartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalOwnerShips_OrderClassId",
                table: "DigitalOwnerShips",
                column: "OrderClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryInfoId",
                table: "Orders",
                column: "DeliveryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUpsells_AuctionProductCartClassId",
                table: "ProductUpsells",
                column: "AuctionProductCartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUpsells_ShoppingCartClassId",
                table: "ProductUpsells",
                column: "ShoppingCartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUpsells_SingleProductCartClassId",
                table: "ProductUpsells",
                column: "SingleProductCartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWithStyleClass_ProductId",
                table: "ProductWithStyleClass",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWithStyleClass_ShippingInfoClassId",
                table: "ProductWithStyleClass",
                column: "ShippingInfoClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProducts_AppointmentClassId",
                table: "ServiceProducts",
                column: "AppointmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_AppointmentClassId",
                table: "Services",
                column: "AppointmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingInfoClass_OrderClassId",
                table: "ShippingInfoClass",
                column: "OrderClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleProductCarts_ItemId",
                table: "SingleProductCarts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleProductImages_SingleProductClassId",
                table: "SingleProductImages",
                column: "SingleProductClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_ProductWithStyleClassId",
                table: "Styles",
                column: "ProductWithStyleClassId");
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
                name: "AuctionProductImages");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyImages");

            migrationBuilder.DropTable(
                name: "CompanyTransactions");

            migrationBuilder.DropTable(
                name: "DigitalOwnerShips");

            migrationBuilder.DropTable(
                name: "FoodCarts");

            migrationBuilder.DropTable(
                name: "FoodImages");

            migrationBuilder.DropTable(
                name: "FoodOrders");

            migrationBuilder.DropTable(
                name: "FoodProducts");

            migrationBuilder.DropTable(
                name: "PointOfContacts");

            migrationBuilder.DropTable(
                name: "PreparedCarts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductUpsells");

            migrationBuilder.DropTable(
                name: "ServiceProducts");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SingleProductImages");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AuctionCarts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "SingleProductCarts");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ProductWithStyleClass");

            migrationBuilder.DropTable(
                name: "AuctionProducts");

            migrationBuilder.DropTable(
                name: "SingleProducts");

            migrationBuilder.DropTable(
                name: "ServiceCarts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShippingInfoClass");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "FoodDeliveryClass");
        }
    }
}
