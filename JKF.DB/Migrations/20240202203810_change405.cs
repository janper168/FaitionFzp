using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change405 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Erp_ChargeOrder",
                columns: table => new
                {
                    Erp_ChargeOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SuppilerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ChargeItemId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChargeItemName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    ChargeAmount = table.Column<int>(type: "int", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_ChargeOrder", x => x.Erp_ChargeOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_Erp_ChargeItem_ChargeItemId",
                        column: x => x.ChargeItemId,
                        principalTable: "Erp_ChargeItem",
                        principalColumn: "Erp_ChargeItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_Erp_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Erp_Customer",
                        principalColumn: "Erp_CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_Erp_Suppiler_SuppilerId",
                        column: x => x.SuppilerId,
                        principalTable: "Erp_Suppiler",
                        principalColumn: "Erp_SuppilerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_ChargeOrder_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_AccountId",
                table: "Erp_ChargeOrder",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_ChargeItemId",
                table: "Erp_ChargeOrder",
                column: "ChargeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_CreateUserId",
                table: "Erp_ChargeOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_CustomerId",
                table: "Erp_ChargeOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_ProcessorId",
                table: "Erp_ChargeOrder",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_ChargeOrder_SuppilerId",
                table: "Erp_ChargeOrder",
                column: "SuppilerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Erp_ChargeOrder");
        }
    }
}
