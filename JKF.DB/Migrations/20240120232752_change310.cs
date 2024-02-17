using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Erp_PurchaseReturnAccount_Erp_PurchaseReturnOrder_PurchaseRe~",
                table: "Erp_PurchaseReturnAccount");

            migrationBuilder.DropIndex(
                name: "IX_Erp_PurchaseReturnAccount_PurchaseReturnOrderId",
                table: "Erp_PurchaseReturnAccount");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseReturnAccountErp_PurchaseReturnAccountId",
                table: "Erp_PurchaseReturnOrder",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount",
                type: "varchar(50)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnOrder_PurchaseReturnAccountErp_PurchaseRet~",
                table: "Erp_PurchaseReturnOrder",
                column: "PurchaseReturnAccountErp_PurchaseReturnAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnAccount_PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_PurchaseReturnAccount_Erp_PurchaseOrder_PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount",
                column: "PurchaseOrderId",
                principalTable: "Erp_PurchaseOrder",
                principalColumn: "Erp_PurchaseOrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_PurchaseReturnOrder_Erp_PurchaseReturnAccount_PurchaseRe~",
                table: "Erp_PurchaseReturnOrder",
                column: "PurchaseReturnAccountErp_PurchaseReturnAccountId",
                principalTable: "Erp_PurchaseReturnAccount",
                principalColumn: "Erp_PurchaseReturnAccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Erp_PurchaseReturnAccount_Erp_PurchaseOrder_PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_Erp_PurchaseReturnOrder_Erp_PurchaseReturnAccount_PurchaseRe~",
                table: "Erp_PurchaseReturnOrder");

            migrationBuilder.DropIndex(
                name: "IX_Erp_PurchaseReturnOrder_PurchaseReturnAccountErp_PurchaseRet~",
                table: "Erp_PurchaseReturnOrder");

            migrationBuilder.DropIndex(
                name: "IX_Erp_PurchaseReturnAccount_PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount");

            migrationBuilder.DropColumn(
                name: "PurchaseReturnAccountErp_PurchaseReturnAccountId",
                table: "Erp_PurchaseReturnOrder");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Erp_PurchaseReturnAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnAccount_PurchaseReturnOrderId",
                table: "Erp_PurchaseReturnAccount",
                column: "PurchaseReturnOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_PurchaseReturnAccount_Erp_PurchaseReturnOrder_PurchaseRe~",
                table: "Erp_PurchaseReturnAccount",
                column: "PurchaseReturnOrderId",
                principalTable: "Erp_PurchaseReturnOrder",
                principalColumn: "Erp_PurchaseReturnOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
