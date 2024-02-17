using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class _323 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Erp_SalesReturnAccount_SalesReturnOrderId",
            //    table: "Erp_SalesReturnAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Erp_StockOutRecord",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Erp_SalesReturnAccount_SalesReturnOrderId",
            //    table: "Erp_SalesReturnAccount",
            //    column: "SalesReturnOrderId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Erp_SalesReturnAccount_SalesReturnOrderId",
            //    table: "Erp_SalesReturnAccount");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "Erp_StockOutRecord",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Erp_SalesReturnAccount_SalesReturnOrderId",
            //    table: "Erp_SalesReturnAccount",
            //    column: "SalesReturnOrderId");
        }
    }
}
