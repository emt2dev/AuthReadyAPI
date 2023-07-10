using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthReadyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

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

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Cart",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "deliveryDriver",
                table: "Orders",
                newName: "delivery_driver_name");

            migrationBuilder.RenameColumn(
                name: "Purchaser",
                table: "Orders",
                newName: "shoppingCartId");

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "delivery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "delivery_driver_latitude",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "delivery_driver_longitdte",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01943585-0fc0-4bb6-9efd-eb2493e6ff1e", null, "User", "USER" },
                    { "29d77218-5cc7-4b16-bee4-30e471c7b257", null, "Company_Admin", "COMPANY_ADMIN" },
                    { "9c71fe2c-9bc1-4979-b7e2-1a3909b4d521", null, "API_Admin", "API_ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01943585-0fc0-4bb6-9efd-eb2493e6ff1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29d77218-5cc7-4b16-bee4-30e471c7b257");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c71fe2c-9bc1-4979-b7e2-1a3909b4d521");

            migrationBuilder.DropColumn(
                name: "delivery",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "delivery_driver_latitude",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "delivery_driver_longitdte",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "shoppingCartId",
                table: "Orders",
                newName: "Purchaser");

            migrationBuilder.RenameColumn(
                name: "delivery_driver_name",
                table: "Orders",
                newName: "deliveryDriver");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cart",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
