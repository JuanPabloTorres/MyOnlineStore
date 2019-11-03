using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOnlineStore.MobileAppService.Migrations
{
    public partial class Db_103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPurchased",
                table: "ProductItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPurchased",
                table: "ProductItems");
        }
    }
}
