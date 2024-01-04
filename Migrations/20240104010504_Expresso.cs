using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class Expresso : Migration
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
                name: "DigitalOwnerShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DigitalOwnerUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DownloadCount = table.Column<int>(type: "int", nullable: false),
                    ProductKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOwnerShips", x => x.Id);
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
                    StripeOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
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
                name: "SerivceCarts",
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
                    table.PrimaryKey("PK_SerivceCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingInfo",
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
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingInfo", x => x.Id);
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
                    DigitalOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
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
                        name: "FK_Appointments_SerivceCarts_ServicesCartClassId",
                        column: x => x.ServicesCartClassId,
                        principalTable: "SerivceCarts",
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
                    ServicesCartClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProducts_SerivceCarts_ServicesCartClassId",
                        column: x => x.ServicesCartClassId,
                        principalTable: "SerivceCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductWithStyleDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingInfoClassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWithStyleDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWithStyleDTO_ShippingInfo_ShippingInfoClassId",
                        column: x => x.ShippingInfoClassId,
                        principalTable: "ShippingInfo",
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
                    CostWithShipping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DigitalOnly = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "074a9e49-9cb3-45f7-a01c-964519645054", null, "Company", "COMPANY" },
                    { "a36dceda-0dd6-4174-81d5-8c60ce217152", null, "User", "USER" },
                    { "c374c6d1-2970-4b3f-84d3-0068e027f863", null, "Admin", "ADMIN" }
                });

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
                name: "IX_ProductWithStyleDTO_ShippingInfoClassId",
                table: "ProductWithStyleDTO",
                column: "ShippingInfoClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProducts_ServicesCartClassId",
                table: "ServiceProducts",
                column: "ServicesCartClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_AppointmentClassId",
                table: "Services",
                column: "AppointmentClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleProductCarts_ItemId",
                table: "SingleProductCarts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleProductImages_SingleProductClassId",
                table: "SingleProductImages",
                column: "SingleProductClassId");
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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PointOfContacts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductUpsells");

            migrationBuilder.DropTable(
                name: "ProductWithStyleDTO");

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
                name: "ShippingInfo");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AuctionProducts");

            migrationBuilder.DropTable(
                name: "SingleProducts");

            migrationBuilder.DropTable(
                name: "SerivceCarts");
        }
    }
}
