using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WarehouseId",
                table: "Erp_Inventory",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Inventory_WarehouseId",
                table: "Erp_Inventory",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_Inventory_Erp_Warehouse_WarehouseId",
                table: "Erp_Inventory",
                column: "WarehouseId",
                principalTable: "Erp_Warehouse",
                principalColumn: "Erp_WarehouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Erp_Inventory_Erp_Warehouse_WarehouseId",
                table: "Erp_Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Erp_Inventory_WarehouseId",
                table: "Erp_Inventory");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Erp_Inventory");
        }
    }
}
