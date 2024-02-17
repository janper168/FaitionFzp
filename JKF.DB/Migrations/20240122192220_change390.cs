using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change390 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockCheckOrderId",
                table: "Erp_InventoryFlow",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VoidStockCheckOrderId",
                table: "Erp_InventoryFlow",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_StockCheckOrderId",
                table: "Erp_InventoryFlow",
                column: "StockCheckOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidStockCheckOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidStockCheckOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_InventoryFlow_Erp_StockCheckOrder_StockCheckOrderId",
                table: "Erp_InventoryFlow",
                column: "StockCheckOrderId",
                principalTable: "Erp_StockCheckOrder",
                principalColumn: "Erp_StockCheckOrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_InventoryFlow_Erp_StockCheckOrder_VoidStockCheckOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidStockCheckOrderId",
                principalTable: "Erp_StockCheckOrder",
                principalColumn: "Erp_StockCheckOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Erp_InventoryFlow_Erp_StockCheckOrder_StockCheckOrderId",
                table: "Erp_InventoryFlow");

            migrationBuilder.DropForeignKey(
                name: "FK_Erp_InventoryFlow_Erp_StockCheckOrder_VoidStockCheckOrderId",
                table: "Erp_InventoryFlow");

            migrationBuilder.DropIndex(
                name: "IX_Erp_InventoryFlow_StockCheckOrderId",
                table: "Erp_InventoryFlow");

            migrationBuilder.DropIndex(
                name: "IX_Erp_InventoryFlow_VoidStockCheckOrderId",
                table: "Erp_InventoryFlow");

            migrationBuilder.DropColumn(
                name: "StockCheckOrderId",
                table: "Erp_InventoryFlow");

            migrationBuilder.DropColumn(
                name: "VoidStockCheckOrderId",
                table: "Erp_InventoryFlow");
        }
    }
}
