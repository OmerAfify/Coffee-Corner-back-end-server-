using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinesssLogic.Migrations
{
    public partial class relationBwOrder_OrderedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderedItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_OrderId",
                table: "OrderedItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderedItems_OrderId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderedItems");
        }
    }
}
