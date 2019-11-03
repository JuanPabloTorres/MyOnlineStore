using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineStore.MobileAppService.Migrations
{
    public partial class Db_102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "ProductItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    StoreOrderId = table.Column<Guid>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PurchaseBy = table.Column<string>(nullable: true),
                    PayedWith = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreOrderId",
                        column: x => x.StoreOrderId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_OrderId",
                table: "ProductItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreOrderId",
                table: "Orders",
                column: "StoreOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Orders_OrderId",
                table: "ProductItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Orders_OrderId",
                table: "ProductItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_OrderId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductItems");
        }
    }
}
