using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change335 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Erp_SalesReturnGoods_Erp_SalesGoods_SalesGoodsId",
                table: "Erp_SalesReturnGoods");

            migrationBuilder.DropIndex(
                name: "IX_Erp_SalesReturnGoods_SalesGoodsId",
                table: "Erp_SalesReturnGoods");

            migrationBuilder.DropColumn(
                name: "SalesGoodsId",
                table: "Erp_SalesReturnGoods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalesGoodsId",
                table: "Erp_SalesReturnGoods",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnGoods_SalesGoodsId",
                table: "Erp_SalesReturnGoods",
                column: "SalesGoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Erp_SalesReturnGoods_Erp_SalesGoods_SalesGoodsId",
                table: "Erp_SalesReturnGoods",
                column: "SalesGoodsId",
                principalTable: "Erp_SalesGoods",
                principalColumn: "Erp_SalesGoodsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
