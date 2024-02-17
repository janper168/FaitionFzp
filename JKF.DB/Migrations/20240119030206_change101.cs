using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JKF.DB.Migrations
{
    public partial class change101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuickQuery = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SimpleSpelling = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Layer = table.Column<int>(type: "int", nullable: false),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateUserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ModifyUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifyUserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authorize",
                columns: table => new
                {
                    AuthorizeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ObjectType = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorize", x => x.AuthorizeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nature = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OuterPhone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InnerPhone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fax = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Postalcode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manager = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinceId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CityId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StreetId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WebAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FoundedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BusinessScope = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DataItem",
                columns: table => new
                {
                    DataItemId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Target = table.Column<int>(type: "int", nullable: false),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataItem", x => x.DataItemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Account",
                columns: table => new
                {
                    Erp_AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Holder = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CardNumber = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actived = table.Column<int>(type: "int", nullable: true),
                    InitialBalanceAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    BalanceStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Account", x => x.Erp_AccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_ChargeItem",
                columns: table => new
                {
                    Erp_ChargeItemId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_ChargeItem", x => x.Erp_ChargeItemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Customer",
                columns: table => new
                {
                    Erp_CustomerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actived = table.Column<int>(type: "int", nullable: false),
                    InitialArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ArearsStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Customer", x => x.Erp_CustomerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_GoodsCategory",
                columns: table => new
                {
                    Erp_GoodsCategoryId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_GoodsCategory", x => x.Erp_GoodsCategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Suppiler",
                columns: table => new
                {
                    Erp_SuppilerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contact = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAccount = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actived = table.Column<int>(type: "int", nullable: false),
                    InitialArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ArearsStatus = table.Column<int>(type: "int", nullable: false),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Suppiler", x => x.Erp_SuppilerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interface",
                columns: table => new
                {
                    InterfaceId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ControllerName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnTypeName = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertiesJson = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interface", x => x.InterfaceId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SourceObjectId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SourceContentJson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    OperateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperateAccount = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperateTypeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OperateType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Module = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IPAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IPAddressName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Host = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Browser = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExecuteResult = table.Column<int>(type: "int", nullable: true),
                    ExecuteResultJson = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ModuleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UrlAddress = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Target = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nature = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manager = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OurPhone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InnerPhone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fax = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DataItemDetail",
                columns: table => new
                {
                    DataItemDetailId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemValue = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<int>(type: "int", nullable: false),
                    SortCode = table.Column<int>(type: "int", nullable: false),
                    DataItemId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataItemDetail", x => x.DataItemDetailId);
                    table.ForeignKey(
                        name: "FK_DataItemDetail_DataItem_DataItemId",
                        column: x => x.DataItemId,
                        principalTable: "DataItem",
                        principalColumn: "DataItemId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Goods",
                columns: table => new
                {
                    Erp_GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BarCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsCategoryId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Spec = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnableBatchControl = table.Column<int>(type: "int", nullable: false),
                    ShelfLifeDays = table.Column<int>(type: "int", nullable: true),
                    ShelfLifeWarningDays = table.Column<int>(type: "int", nullable: false),
                    EnableInventoryWarning = table.Column<int>(type: "int", nullable: false),
                    InventoryUpper = table.Column<int>(type: "int", nullable: true),
                    InventoryLower = table.Column<int>(type: "int", nullable: true),
                    RetailPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LevelPrice1 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LevelPrice2 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LevelPrice3 = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actived = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Goods", x => x.Erp_GoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_Goods_Erp_GoodsCategory_GoodsCategoryId",
                        column: x => x.GoodsCategoryId,
                        principalTable: "Erp_GoodsCategory",
                        principalColumn: "Erp_GoodsCategoryId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DataAuthorize",
                columns: table => new
                {
                    DataAuthorizeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InterfaceId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ObjectType = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionsJson = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Formula = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    DeleteMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataAuthorize", x => x.DataAuthorizeId);
                    table.ForeignKey(
                        name: "FK_DataAuthorize_Interface_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "Interface",
                        principalColumn: "InterfaceId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleButton",
                columns: table => new
                {
                    ModuleButtonId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Target = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnabledMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleButton", x => x.ModuleButtonId);
                    table.ForeignKey(
                        name: "FK_ModuleButton_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleColumn",
                columns: table => new
                {
                    ModuleColumnId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnabledMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleColumn", x => x.ModuleColumnId);
                    table.ForeignKey(
                        name: "FK_ModuleColumn_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleExcelImportConfig",
                columns: table => new
                {
                    ModuleExcelImportConfigId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PropertyType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPhysics = table.Column<int>(type: "int", nullable: false),
                    IsParentId = table.Column<int>(type: "int", nullable: false),
                    IsRefer = table.Column<int>(type: "int", nullable: false),
                    RefEntityName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefPropertyName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefEntityServiceName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsValid = table.Column<int>(type: "int", nullable: false),
                    IsArea = table.Column<int>(type: "int", nullable: false),
                    AreaLayer = table.Column<int>(type: "int", nullable: false),
                    IsDataItem = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SortCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleExcelImportConfig", x => x.ModuleExcelImportConfigId);
                    table.ForeignKey(
                        name: "FK_ModuleExcelImportConfig_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ModuleForm",
                columns: table => new
                {
                    ModuleFormId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuleId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnabledMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleForm", x => x.ModuleFormId);
                    table.ForeignKey(
                        name: "FK_ModuleForm_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NickName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HeadIcon = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuickQuery = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SimpleSpelling = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OICQ = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeChat = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MSN = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeleteMark = table.Column<int>(type: "int", nullable: false),
                    EnabledMark = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_GoodsImage",
                columns: table => new
                {
                    Erp_GoodsImageId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_GoodsImage", x => x.Erp_GoodsImageId);
                    table.ForeignKey(
                        name: "FK_Erp_GoodsImage_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Inventory",
                columns: table => new
                {
                    Erp_InventoryId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InitialQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    StockStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Inventory", x => x.Erp_InventoryId);
                    table.ForeignKey(
                        name: "FK_Erp_Inventory_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomizedForm",
                columns: table => new
                {
                    CustomizedFormId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FormName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FormCfg = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomizedForm", x => x.CustomizedFormId);
                    table.ForeignKey(
                        name: "FK_CustomizedForm_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomizedForm_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_AccountTransferRecord",
                columns: table => new
                {
                    Erp_AccountTransferRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChargePayer = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OutAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OutTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ChargeAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_AccountTransferRecord", x => x.Erp_AccountTransferRecordId);
                    table.ForeignKey(
                        name: "FK_Erp_AccountTransferRecord_Erp_Account_InAccountId",
                        column: x => x.InAccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_AccountTransferRecord_Erp_Account_OutAccountId",
                        column: x => x.OutAccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_AccountTransferRecord_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_AccountTransferRecord_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_CollectionOrder",
                columns: table => new
                {
                    Erp_CollectionOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<int>(type: "int", nullable: false),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_CollectionOrder", x => x.Erp_CollectionOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_CollectionOrder_Erp_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Erp_Customer",
                        principalColumn: "Erp_CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_CollectionOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_CollectionOrder_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PaymentOrder",
                columns: table => new
                {
                    Erp_PaymentOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SuppilerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<int>(type: "int", nullable: false),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PaymentOrder", x => x.Erp_PaymentOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_PaymentOrder_Erp_Suppiler_SuppilerId",
                        column: x => x.SuppilerId,
                        principalTable: "Erp_Suppiler",
                        principalColumn: "Erp_SuppilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PaymentOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PaymentOrder_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Warehouse",
                columns: table => new
                {
                    Erp_WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManagerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actived = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Warehouse", x => x.Erp_WarehouseId);
                    table.ForeignKey(
                        name: "FK_Erp_Warehouse_User_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostUser",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostUser_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowDesign",
                columns: table => new
                {
                    WorkFlowDesignId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomizedFormId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlowDesignJson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowDesign", x => x.WorkFlowDesignId);
                    table.ForeignKey(
                        name: "FK_WorkFlowDesign_CustomizedForm_CustomizedFormId",
                        column: x => x.CustomizedFormId,
                        principalTable: "CustomizedForm",
                        principalColumn: "CustomizedFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFlowDesign_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFlowDesign_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_CollectionAccount",
                columns: table => new
                {
                    Erp_CollectionAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_CollectionAccount", x => x.Erp_CollectionAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_CollectionAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_CollectionAccount_Erp_CollectionOrder_CollectionOrderId",
                        column: x => x.CollectionOrderId,
                        principalTable: "Erp_CollectionOrder",
                        principalColumn: "Erp_CollectionOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PaymentAccount",
                columns: table => new
                {
                    Erp_PaymentAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PaymentAccount", x => x.Erp_PaymentAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_PaymentAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PaymentAccount_Erp_PaymentOrder_PaymentOrderId",
                        column: x => x.PaymentOrderId,
                        principalTable: "Erp_PaymentOrder",
                        principalColumn: "Erp_PaymentOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_Batch",
                columns: table => new
                {
                    Erp_BatchId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InventoryId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InitialQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ShelfLifeDays = table.Column<int>(type: "int", nullable: true),
                    WraningDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StockStatus = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_Batch", x => x.Erp_BatchId);
                    table.ForeignKey(
                        name: "FK_Erp_Batch_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_Batch_Erp_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Erp_Inventory",
                        principalColumn: "Erp_InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_Batch_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseOrder",
                columns: table => new
                {
                    Erp_PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SuppilerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    otherAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockIn = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseOrder", x => x.Erp_PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseOrder_Erp_Suppiler_SuppilerId",
                        column: x => x.SuppilerId,
                        principalTable: "Erp_Suppiler",
                        principalColumn: "Erp_SuppilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesOrder",
                columns: table => new
                {
                    Erp_SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<float>(type: "float", nullable: false),
                    otherAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CollectionAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockOut = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesOrder", x => x.Erp_SalesOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesOrder_Erp_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Erp_Customer",
                        principalColumn: "Erp_CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockCheckOrder",
                columns: table => new
                {
                    Erp_StockCheckOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalBookQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalActualQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalSurplusQuantity = table.Column<int>(type: "int", nullable: true),
                    TotalSurplusAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockCheckOrder", x => x.Erp_StockCheckOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckOrder_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockTransferOrder",
                columns: table => new
                {
                    Erp_StockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OutWarehouseId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InWarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockIn = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockOut = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockTransferOrder", x => x.Erp_StockTransferOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_StockTransferOrder_Erp_Warehouse_InWarehouseId",
                        column: x => x.InWarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockTransferOrder_Erp_Warehouse_OutWarehouseId",
                        column: x => x.OutWarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockTransferOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowDesignId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplyerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FormContentJson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    ParentTaskId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Task_User_ApplyerId",
                        column: x => x.ApplyerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_WorkFlowDesign_WorkFlowDesignId",
                        column: x => x.WorkFlowDesignId,
                        principalTable: "WorkFlowDesign",
                        principalColumn: "WorkFlowDesignId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowLine",
                columns: table => new
                {
                    WorkFlowLineId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowDesignId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LineId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LineName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LineType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowLine", x => x.WorkFlowLineId);
                    table.ForeignKey(
                        name: "FK_WorkFlowLine_WorkFlowDesign_WorkFlowDesignId",
                        column: x => x.WorkFlowDesignId,
                        principalTable: "WorkFlowDesign",
                        principalColumn: "WorkFlowDesignId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkFlowNode",
                columns: table => new
                {
                    WorkFlowNodeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowDesignId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAllPassThenPassRule = table.Column<int>(type: "int", nullable: true),
                    IsProcessorTheSameToApplyerPassRule = table.Column<int>(type: "int", nullable: true),
                    IsProcessorTheSameToLastProcessorRule = table.Column<int>(type: "int", nullable: true),
                    ProcessorTypeRule = table.Column<int>(type: "int", nullable: true),
                    ProcessorIds = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChildFlowDesignId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionRuleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowNode", x => x.WorkFlowNodeId);
                    table.ForeignKey(
                        name: "FK_WorkFlowNode_WorkFlowDesign_WorkFlowDesignId",
                        column: x => x.WorkFlowDesignId,
                        principalTable: "WorkFlowDesign",
                        principalColumn: "WorkFlowDesignId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseAccount",
                columns: table => new
                {
                    Erp_PurchaseAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseAccount", x => x.Erp_PurchaseAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseAccount_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseGoods",
                columns: table => new
                {
                    Erp_PurchaseGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReturnQuantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseGoods", x => x.Erp_PurchaseGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseGoods_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseReturnOrder",
                columns: table => new
                {
                    Erp_PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SuppilerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    otherAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    CollectionAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockOut = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseReturnOrder", x => x.Erp_PurchaseReturnOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnOrder_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnOrder_Erp_Suppiler_SuppilerId",
                        column: x => x.SuppilerId,
                        principalTable: "Erp_Suppiler",
                        principalColumn: "Erp_SuppilerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesAccount",
                columns: table => new
                {
                    Erp_SalesAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesAccount", x => x.Erp_SalesAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesAccount_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesGoods",
                columns: table => new
                {
                    Erp_SalesGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesQuantity = table.Column<int>(type: "int", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ReturnQuantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesGoods", x => x.Erp_SalesGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesGoods_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesReturnOrder",
                columns: table => new
                {
                    Erp_SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    otherAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    ArearsAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    EnableAutoStockIn = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesReturnOrder", x => x.Erp_SalesReturnOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnOrder_Erp_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Erp_Customer",
                        principalColumn: "Erp_CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnOrder_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockCheckGoods",
                columns: table => new
                {
                    Erp_StockCheckGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockCheckOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookQuantity = table.Column<int>(type: "int", nullable: false),
                    ActualQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SurplusQuantity = table.Column<int>(type: "int", nullable: false),
                    SurplusAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockCheckGoods", x => x.Erp_StockCheckGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckGoods_Erp_StockCheckOrder_StockCheckOrderId",
                        column: x => x.StockCheckOrderId,
                        principalTable: "Erp_StockCheckOrder",
                        principalColumn: "Erp_StockCheckOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckGoods_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockTransferGoods",
                columns: table => new
                {
                    Erp_StockTransferGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockTransferQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockTransferGoods", x => x.Erp_StockTransferGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockTransferGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockTransferGoods_Erp_StockTransferOrder_StockTransferO~",
                        column: x => x.StockTransferOrderId,
                        principalTable: "Erp_StockTransferOrder",
                        principalColumn: "Erp_StockTransferOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskNode",
                columns: table => new
                {
                    TaskNodeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeId = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcesserIds = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeResult = table.Column<int>(type: "int", nullable: false),
                    CerateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNode", x => x.TaskNodeId);
                    table.ForeignKey(
                        name: "FK_TaskNode_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConditionRule",
                columns: table => new
                {
                    ConditionRuleId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkFlowNodeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RulesJson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionRule", x => x.ConditionRuleId);
                    table.ForeignKey(
                        name: "FK_ConditionRule_WorkFlowNode_WorkFlowNodeId",
                        column: x => x.WorkFlowNodeId,
                        principalTable: "WorkFlowNode",
                        principalColumn: "WorkFlowNodeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseReturnAccount",
                columns: table => new
                {
                    Erp_PurchaseReturnAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseReturnAccount", x => x.Erp_PurchaseReturnAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnAccount_Erp_PurchaseReturnOrder_PurchaseRe~",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_PurchaseReturnGoods",
                columns: table => new
                {
                    Erp_PurchaseReturnGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnQuantity = table.Column<int>(type: "int", nullable: false),
                    ReturnPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_PurchaseReturnGoods", x => x.Erp_PurchaseReturnGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_PurchaseReturnGoods_Erp_PurchaseReturnOrder_PurchaseRetu~",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockOutOrder",
                columns: table => new
                {
                    Erp_StockOutOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainQuantity = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockOutOrder", x => x.Erp_StockOutOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutOrder_Erp_PurchaseReturnOrder_PurchaseReturnOrde~",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutOrder_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutOrder_Erp_StockTransferOrder_StockTransferOrderId",
                        column: x => x.StockTransferOrderId,
                        principalTable: "Erp_StockTransferOrder",
                        principalColumn: "Erp_StockTransferOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_FinanceFlow",
                columns: table => new
                {
                    Erp_FinanceFlowId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AmountBefore = table.Column<int>(type: "int", nullable: false),
                    AmountChange = table.Column<int>(type: "int", nullable: false),
                    AmountAfter = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidPurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidPurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidSalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidSalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidPaymentOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidCollectionOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountTransferRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidAccountTransferRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_FinanceFlow", x => x.Erp_FinanceFlowId);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_AccountTransferRecord_AccountTransferRec~",
                        column: x => x.AccountTransferRecordId,
                        principalTable: "Erp_AccountTransferRecord",
                        principalColumn: "Erp_AccountTransferRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_AccountTransferRecord_VoidAccountTransfe~",
                        column: x => x.VoidAccountTransferRecordId,
                        principalTable: "Erp_AccountTransferRecord",
                        principalColumn: "Erp_AccountTransferRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_CollectionOrder_CollectionOrderId",
                        column: x => x.CollectionOrderId,
                        principalTable: "Erp_CollectionOrder",
                        principalColumn: "Erp_CollectionOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_CollectionOrder_VoidCollectionOrderId",
                        column: x => x.VoidCollectionOrderId,
                        principalTable: "Erp_CollectionOrder",
                        principalColumn: "Erp_CollectionOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PaymentOrder_PaymentOrderId",
                        column: x => x.PaymentOrderId,
                        principalTable: "Erp_PaymentOrder",
                        principalColumn: "Erp_PaymentOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PaymentOrder_VoidPaymentOrderId",
                        column: x => x.VoidPaymentOrderId,
                        principalTable: "Erp_PaymentOrder",
                        principalColumn: "Erp_PaymentOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PurchaseOrder_VoidPurchaseOrderId",
                        column: x => x.VoidPurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PurchaseReturnOrder_PurchaseReturnOrderId",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_PurchaseReturnOrder_VoidPurchaseReturnOr~",
                        column: x => x.VoidPurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_SalesOrder_VoidSalesOrderId",
                        column: x => x.VoidSalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_SalesReturnOrder_SalesReturnOrderId",
                        column: x => x.SalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_Erp_SalesReturnOrder_VoidSalesReturnOrderId",
                        column: x => x.VoidSalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_FinanceFlow_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesReturnAccount",
                columns: table => new
                {
                    Erp_SalesReturnAccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesReturnAccount", x => x.Erp_SalesReturnAccountId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnAccount_Erp_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Erp_Account",
                        principalColumn: "Erp_AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnAccount_Erp_SalesReturnOrder_SalesReturnOrder~",
                        column: x => x.SalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_SalesReturnGoods",
                columns: table => new
                {
                    Erp_SalesReturnGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnQuantity = table.Column<int>(type: "int", nullable: false),
                    ReturnPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_SalesReturnGoods", x => x.Erp_SalesReturnGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnGoods_Erp_SalesGoods_SalesGoodsId",
                        column: x => x.SalesGoodsId,
                        principalTable: "Erp_SalesGoods",
                        principalColumn: "Erp_SalesGoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_SalesReturnGoods_Erp_SalesReturnOrder_SalesReturnOrderId",
                        column: x => x.SalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockInOrder",
                columns: table => new
                {
                    Erp_StockInOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainQuantity = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockInOrder", x => x.Erp_StockInOrderId);
                    table.ForeignKey(
                        name: "FK_Erp_StockInOrder_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockInOrder_Erp_SalesReturnOrder_SalesReturnOrderId",
                        column: x => x.SalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockInOrder_Erp_StockTransferOrder_StockTransferOrderId",
                        column: x => x.StockTransferOrderId,
                        principalTable: "Erp_StockTransferOrder",
                        principalColumn: "Erp_StockTransferOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockInOrder_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInOrder_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockCheckBatch",
                columns: table => new
                {
                    Erp_StockCheckBatchId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockCheckOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockCheckGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BatchNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookQuantity = table.Column<int>(type: "int", nullable: false),
                    ActualQuantity = table.Column<int>(type: "int", nullable: false),
                    SurplusQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockCheckBatch", x => x.Erp_StockCheckBatchId);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckBatch_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckBatch_Erp_StockCheckGoods_StockCheckGoodsId",
                        column: x => x.StockCheckGoodsId,
                        principalTable: "Erp_StockCheckGoods",
                        principalColumn: "Erp_StockCheckGoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockCheckBatch_Erp_StockCheckOrder_StockCheckOrderId",
                        column: x => x.StockCheckOrderId,
                        principalTable: "Erp_StockCheckOrder",
                        principalColumn: "Erp_StockCheckOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskNodeProcesser",
                columns: table => new
                {
                    TaskNodeProcesserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcesserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CerateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProcessResult = table.Column<int>(type: "int", nullable: false),
                    ProcessComment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNodeProcesser", x => x.TaskNodeProcesserId);
                    table.ForeignKey(
                        name: "FK_TaskNodeProcesser_TaskNode_TaskNodeId",
                        column: x => x.TaskNodeId,
                        principalTable: "TaskNode",
                        principalColumn: "TaskNodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskNodeProcesser_User_ProcesserId",
                        column: x => x.ProcesserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockOutGoods",
                columns: table => new
                {
                    Erp_StockOutGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainQuantity = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockOutGoods", x => x.Erp_StockOutGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutGoods_Erp_StockOutOrder_StockOutOrderId",
                        column: x => x.StockOutOrderId,
                        principalTable: "Erp_StockOutOrder",
                        principalColumn: "Erp_StockOutOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockOutRecord",
                columns: table => new
                {
                    Erp_StockOutRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessorId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockOutRecord", x => x.Erp_StockOutRecordId);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecord_Erp_StockOutOrder_StockOutOrderId",
                        column: x => x.StockOutOrderId,
                        principalTable: "Erp_StockOutOrder",
                        principalColumn: "Erp_StockOutOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecord_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecord_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecord_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_InventoryFlow",
                columns: table => new
                {
                    Erp_InventoryFlowId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantityBefore = table.Column<int>(type: "int", nullable: false),
                    QuantityChange = table.Column<int>(type: "int", nullable: false),
                    QuantityAfter = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidPurchaseOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidPurchaseReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidSalesOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidSalesReturnOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidStockInOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidStockOutOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VoidStockTransferOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_InventoryFlow", x => x.Erp_InventoryFlowId);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_PurchaseOrder_VoidPurchaseOrderId",
                        column: x => x.VoidPurchaseOrderId,
                        principalTable: "Erp_PurchaseOrder",
                        principalColumn: "Erp_PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_PurchaseReturnOrder_PurchaseReturnOrde~",
                        column: x => x.PurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_PurchaseReturnOrder_VoidPurchaseReturn~",
                        column: x => x.VoidPurchaseReturnOrderId,
                        principalTable: "Erp_PurchaseReturnOrder",
                        principalColumn: "Erp_PurchaseReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_SalesOrder_VoidSalesOrderId",
                        column: x => x.VoidSalesOrderId,
                        principalTable: "Erp_SalesOrder",
                        principalColumn: "Erp_SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_SalesReturnOrder_SalesReturnOrderId",
                        column: x => x.SalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_SalesReturnOrder_VoidSalesReturnOrderId",
                        column: x => x.VoidSalesReturnOrderId,
                        principalTable: "Erp_SalesReturnOrder",
                        principalColumn: "Erp_SalesReturnOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockInOrder_StockInOrderId",
                        column: x => x.StockInOrderId,
                        principalTable: "Erp_StockInOrder",
                        principalColumn: "Erp_StockInOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockInOrder_VoidStockInOrderId",
                        column: x => x.VoidStockInOrderId,
                        principalTable: "Erp_StockInOrder",
                        principalColumn: "Erp_StockInOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockOutOrder_StockOutOrderId",
                        column: x => x.StockOutOrderId,
                        principalTable: "Erp_StockOutOrder",
                        principalColumn: "Erp_StockOutOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockOutOrder_VoidStockOutOrderId",
                        column: x => x.VoidStockOutOrderId,
                        principalTable: "Erp_StockOutOrder",
                        principalColumn: "Erp_StockOutOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockTransferOrder_StockTransferOrderId",
                        column: x => x.StockTransferOrderId,
                        principalTable: "Erp_StockTransferOrder",
                        principalColumn: "Erp_StockTransferOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_StockTransferOrder_VoidStockTransferOr~",
                        column: x => x.VoidStockTransferOrderId,
                        principalTable: "Erp_StockTransferOrder",
                        principalColumn: "Erp_StockTransferOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_InventoryFlow_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockInGoods",
                columns: table => new
                {
                    Erp_StockInGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainQuantity = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockInGoods", x => x.Erp_StockInGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockInGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInGoods_Erp_StockInOrder_StockInOrderId",
                        column: x => x.StockInOrderId,
                        principalTable: "Erp_StockInOrder",
                        principalColumn: "Erp_StockInOrderId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockInRecord",
                columns: table => new
                {
                    Erp_StockInRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WarehouseId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInOrderId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProcessorId = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVoid = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockInRecord", x => x.Erp_StockInRecordId);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecord_Erp_StockInOrder_StockInOrderId",
                        column: x => x.StockInOrderId,
                        principalTable: "Erp_StockInOrder",
                        principalColumn: "Erp_StockInOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecord_Erp_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Erp_Warehouse",
                        principalColumn: "Erp_WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecord_User_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecord_User_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskNodeLog",
                columns: table => new
                {
                    TaskNodeLogId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeProcesserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskNodeName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProcessResult = table.Column<int>(type: "int", nullable: false),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProcessComment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskNodeLog", x => x.TaskNodeLogId);
                    table.ForeignKey(
                        name: "FK_TaskNodeLog_TaskNode_TaskNodeId",
                        column: x => x.TaskNodeId,
                        principalTable: "TaskNode",
                        principalColumn: "TaskNodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskNodeLog_TaskNodeProcesser_TaskNodeProcesserId",
                        column: x => x.TaskNodeProcesserId,
                        principalTable: "TaskNodeProcesser",
                        principalColumn: "TaskNodeProcesserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockOutRecordGoods",
                columns: table => new
                {
                    Erp_StockOutRecordGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BatchId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockOutQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockOutRecordGoods", x => x.Erp_StockOutRecordGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecordGoods_Erp_Batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Erp_Batch",
                        principalColumn: "Erp_BatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecordGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecordGoods_Erp_StockOutGoods_StockOutGoodsId",
                        column: x => x.StockOutGoodsId,
                        principalTable: "Erp_StockOutGoods",
                        principalColumn: "Erp_StockOutGoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockOutRecordGoods_Erp_StockOutRecord_StockOutRecordId",
                        column: x => x.StockOutRecordId,
                        principalTable: "Erp_StockOutRecord",
                        principalColumn: "Erp_StockOutRecordId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Erp_StockInRecordGoods",
                columns: table => new
                {
                    Erp_StockInRecordGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInGoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInRecordId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoodsId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BatchId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StockInQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erp_StockInRecordGoods", x => x.Erp_StockInRecordGoodsId);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecordGoods_Erp_Batch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "Erp_Batch",
                        principalColumn: "Erp_BatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecordGoods_Erp_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Erp_Goods",
                        principalColumn: "Erp_GoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecordGoods_Erp_StockInGoods_StockInGoodsId",
                        column: x => x.StockInGoodsId,
                        principalTable: "Erp_StockInGoods",
                        principalColumn: "Erp_StockInGoodsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Erp_StockInRecordGoods_Erp_StockInRecord_StockInRecordId",
                        column: x => x.StockInRecordId,
                        principalTable: "Erp_StockInRecord",
                        principalColumn: "Erp_StockInRecordId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionRule_WorkFlowNodeId",
                table: "ConditionRule",
                column: "WorkFlowNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizedForm_CreateUserId",
                table: "CustomizedForm",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomizedForm_UpdateUserId",
                table: "CustomizedForm",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataAuthorize_InterfaceId",
                table: "DataAuthorize",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataItemDetail_DataItemId",
                table: "DataItemDetail",
                column: "DataItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_AccountTransferRecord_CreateUserId",
                table: "Erp_AccountTransferRecord",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_AccountTransferRecord_InAccountId",
                table: "Erp_AccountTransferRecord",
                column: "InAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_AccountTransferRecord_OutAccountId",
                table: "Erp_AccountTransferRecord",
                column: "OutAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_AccountTransferRecord_ProcessorId",
                table: "Erp_AccountTransferRecord",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Batch_GoodsId",
                table: "Erp_Batch",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Batch_InventoryId",
                table: "Erp_Batch",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Batch_WarehouseId",
                table: "Erp_Batch",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_CollectionAccount_AccountId",
                table: "Erp_CollectionAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_CollectionAccount_CollectionOrderId",
                table: "Erp_CollectionAccount",
                column: "CollectionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_CollectionOrder_CreateUserId",
                table: "Erp_CollectionOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_CollectionOrder_CustomerId",
                table: "Erp_CollectionOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_CollectionOrder_ProcessorId",
                table: "Erp_CollectionOrder",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_AccountId",
                table: "Erp_FinanceFlow",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_AccountTransferRecordId",
                table: "Erp_FinanceFlow",
                column: "AccountTransferRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_CollectionOrderId",
                table: "Erp_FinanceFlow",
                column: "CollectionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_CreateUserId",
                table: "Erp_FinanceFlow",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_PaymentOrderId",
                table: "Erp_FinanceFlow",
                column: "PaymentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_PurchaseOrderId",
                table: "Erp_FinanceFlow",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_PurchaseReturnOrderId",
                table: "Erp_FinanceFlow",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_SalesOrderId",
                table: "Erp_FinanceFlow",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_SalesReturnOrderId",
                table: "Erp_FinanceFlow",
                column: "SalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidAccountTransferRecordId",
                table: "Erp_FinanceFlow",
                column: "VoidAccountTransferRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidCollectionOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidCollectionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidPaymentOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidPaymentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidPurchaseOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidPurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidPurchaseReturnOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidPurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidSalesOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_FinanceFlow_VoidSalesReturnOrderId",
                table: "Erp_FinanceFlow",
                column: "VoidSalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Goods_GoodsCategoryId",
                table: "Erp_Goods",
                column: "GoodsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_GoodsImage_GoodsId",
                table: "Erp_GoodsImage",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Inventory_GoodsId",
                table: "Erp_Inventory",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_CreateUserId",
                table: "Erp_InventoryFlow",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_GoodsId",
                table: "Erp_InventoryFlow",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_PurchaseOrderId",
                table: "Erp_InventoryFlow",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_PurchaseReturnOrderId",
                table: "Erp_InventoryFlow",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_SalesOrderId",
                table: "Erp_InventoryFlow",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_SalesReturnOrderId",
                table: "Erp_InventoryFlow",
                column: "SalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_StockInOrderId",
                table: "Erp_InventoryFlow",
                column: "StockInOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_StockOutOrderId",
                table: "Erp_InventoryFlow",
                column: "StockOutOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_StockTransferOrderId",
                table: "Erp_InventoryFlow",
                column: "StockTransferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidPurchaseOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidPurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidPurchaseReturnOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidPurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidSalesOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidSalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidSalesReturnOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidSalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidStockInOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidStockInOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidStockOutOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidStockOutOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_VoidStockTransferOrderId",
                table: "Erp_InventoryFlow",
                column: "VoidStockTransferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_InventoryFlow_WarehouseId",
                table: "Erp_InventoryFlow",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PaymentAccount_AccountId",
                table: "Erp_PaymentAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PaymentAccount_PaymentOrderId",
                table: "Erp_PaymentAccount",
                column: "PaymentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PaymentOrder_CreateUserId",
                table: "Erp_PaymentOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PaymentOrder_ProcessorId",
                table: "Erp_PaymentOrder",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PaymentOrder_SuppilerId",
                table: "Erp_PaymentOrder",
                column: "SuppilerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseAccount_AccountId",
                table: "Erp_PurchaseAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseAccount_PurchaseOrderId",
                table: "Erp_PurchaseAccount",
                column: "PurchaseOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseGoods_GoodsId",
                table: "Erp_PurchaseGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseGoods_PurchaseOrderId",
                table: "Erp_PurchaseGoods",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseOrder_CreateUserId",
                table: "Erp_PurchaseOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseOrder_SuppilerId",
                table: "Erp_PurchaseOrder",
                column: "SuppilerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseOrder_WarehouseId",
                table: "Erp_PurchaseOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnAccount_AccountId",
                table: "Erp_PurchaseReturnAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnAccount_PurchaseReturnOrderId",
                table: "Erp_PurchaseReturnAccount",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnGoods_GoodsId",
                table: "Erp_PurchaseReturnGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnGoods_PurchaseReturnOrderId",
                table: "Erp_PurchaseReturnGoods",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnOrder_CreateUserId",
                table: "Erp_PurchaseReturnOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnOrder_PurchaseOrderId",
                table: "Erp_PurchaseReturnOrder",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnOrder_SuppilerId",
                table: "Erp_PurchaseReturnOrder",
                column: "SuppilerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_PurchaseReturnOrder_WarehouseId",
                table: "Erp_PurchaseReturnOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesAccount_AccountId",
                table: "Erp_SalesAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesAccount_SalesOrderId",
                table: "Erp_SalesAccount",
                column: "SalesOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesGoods_GoodsId",
                table: "Erp_SalesGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesGoods_SalesOrderId",
                table: "Erp_SalesGoods",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesOrder_CreateUserId",
                table: "Erp_SalesOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesOrder_CustomerId",
                table: "Erp_SalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesOrder_WarehouseId",
                table: "Erp_SalesOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnAccount_AccountId",
                table: "Erp_SalesReturnAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnAccount_SalesReturnOrderId",
                table: "Erp_SalesReturnAccount",
                column: "SalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnGoods_GoodsId",
                table: "Erp_SalesReturnGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnGoods_SalesGoodsId",
                table: "Erp_SalesReturnGoods",
                column: "SalesGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnGoods_SalesReturnOrderId",
                table: "Erp_SalesReturnGoods",
                column: "SalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnOrder_CreateUserId",
                table: "Erp_SalesReturnOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnOrder_CustomerId",
                table: "Erp_SalesReturnOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnOrder_SalesOrderId",
                table: "Erp_SalesReturnOrder",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_SalesReturnOrder_WarehouseId",
                table: "Erp_SalesReturnOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckBatch_GoodsId",
                table: "Erp_StockCheckBatch",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckBatch_StockCheckGoodsId",
                table: "Erp_StockCheckBatch",
                column: "StockCheckGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckBatch_StockCheckOrderId",
                table: "Erp_StockCheckBatch",
                column: "StockCheckOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckGoods_CreateUserId",
                table: "Erp_StockCheckGoods",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckGoods_GoodsId",
                table: "Erp_StockCheckGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckGoods_StockCheckOrderId",
                table: "Erp_StockCheckGoods",
                column: "StockCheckOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckOrder_CreateUserId",
                table: "Erp_StockCheckOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckOrder_ProcessorId",
                table: "Erp_StockCheckOrder",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockCheckOrder_WarehouseId",
                table: "Erp_StockCheckOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInGoods_GoodsId",
                table: "Erp_StockInGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInGoods_StockInOrderId",
                table: "Erp_StockInGoods",
                column: "StockInOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInOrder_CreateUserId",
                table: "Erp_StockInOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInOrder_PurchaseOrderId",
                table: "Erp_StockInOrder",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInOrder_SalesReturnOrderId",
                table: "Erp_StockInOrder",
                column: "SalesReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInOrder_StockTransferOrderId",
                table: "Erp_StockInOrder",
                column: "StockTransferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInOrder_WarehouseId",
                table: "Erp_StockInOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecord_CreateUserId",
                table: "Erp_StockInRecord",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecord_ProcessorId",
                table: "Erp_StockInRecord",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecord_StockInOrderId",
                table: "Erp_StockInRecord",
                column: "StockInOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecord_WarehouseId",
                table: "Erp_StockInRecord",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecordGoods_BatchId",
                table: "Erp_StockInRecordGoods",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecordGoods_GoodsId",
                table: "Erp_StockInRecordGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecordGoods_StockInGoodsId",
                table: "Erp_StockInRecordGoods",
                column: "StockInGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockInRecordGoods_StockInRecordId",
                table: "Erp_StockInRecordGoods",
                column: "StockInRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutGoods_GoodsId",
                table: "Erp_StockOutGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutGoods_StockOutOrderId",
                table: "Erp_StockOutGoods",
                column: "StockOutOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutOrder_CreateUserId",
                table: "Erp_StockOutOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutOrder_PurchaseReturnOrderId",
                table: "Erp_StockOutOrder",
                column: "PurchaseReturnOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutOrder_SalesOrderId",
                table: "Erp_StockOutOrder",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutOrder_StockTransferOrderId",
                table: "Erp_StockOutOrder",
                column: "StockTransferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutOrder_WarehouseId",
                table: "Erp_StockOutOrder",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecord_CreateUserId",
                table: "Erp_StockOutRecord",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecord_ProcessorId",
                table: "Erp_StockOutRecord",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecord_StockOutOrderId",
                table: "Erp_StockOutRecord",
                column: "StockOutOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecord_WarehouseId",
                table: "Erp_StockOutRecord",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecordGoods_BatchId",
                table: "Erp_StockOutRecordGoods",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecordGoods_GoodsId",
                table: "Erp_StockOutRecordGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecordGoods_StockOutGoodsId",
                table: "Erp_StockOutRecordGoods",
                column: "StockOutGoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockOutRecordGoods_StockOutRecordId",
                table: "Erp_StockOutRecordGoods",
                column: "StockOutRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockTransferGoods_GoodsId",
                table: "Erp_StockTransferGoods",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockTransferGoods_StockTransferOrderId",
                table: "Erp_StockTransferGoods",
                column: "StockTransferOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockTransferOrder_CreateUserId",
                table: "Erp_StockTransferOrder",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockTransferOrder_InWarehouseId",
                table: "Erp_StockTransferOrder",
                column: "InWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_StockTransferOrder_OutWarehouseId",
                table: "Erp_StockTransferOrder",
                column: "OutWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Erp_Warehouse_ManagerId",
                table: "Erp_Warehouse",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleButton_ModuleId",
                table: "ModuleButton",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleColumn_ModuleId",
                table: "ModuleColumn",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleExcelImportConfig_ModuleId",
                table: "ModuleExcelImportConfig",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleForm_ModuleId",
                table: "ModuleForm",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_DepartmentId",
                table: "Post",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_UserId",
                table: "PostUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ApplyerId",
                table: "Task",
                column: "ApplyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_WorkFlowDesignId",
                table: "Task",
                column: "WorkFlowDesignId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNode_TaskId",
                table: "TaskNode",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNodeLog_TaskNodeId",
                table: "TaskNodeLog",
                column: "TaskNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNodeLog_TaskNodeProcesserId",
                table: "TaskNodeLog",
                column: "TaskNodeProcesserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNodeProcesser_ProcesserId",
                table: "TaskNodeProcesser",
                column: "ProcesserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNodeProcesser_TaskNodeId",
                table: "TaskNodeProcesser",
                column: "TaskNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDesign_CreateUserId",
                table: "WorkFlowDesign",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDesign_CustomizedFormId",
                table: "WorkFlowDesign",
                column: "CustomizedFormId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowDesign_UpdateUserId",
                table: "WorkFlowDesign",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowLine_WorkFlowDesignId",
                table: "WorkFlowLine",
                column: "WorkFlowDesignId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowNode_WorkFlowDesignId",
                table: "WorkFlowNode",
                column: "WorkFlowDesignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Authorize");

            migrationBuilder.DropTable(
                name: "ConditionRule");

            migrationBuilder.DropTable(
                name: "DataAuthorize");

            migrationBuilder.DropTable(
                name: "DataItemDetail");

            migrationBuilder.DropTable(
                name: "Erp_ChargeItem");

            migrationBuilder.DropTable(
                name: "Erp_CollectionAccount");

            migrationBuilder.DropTable(
                name: "Erp_FinanceFlow");

            migrationBuilder.DropTable(
                name: "Erp_GoodsImage");

            migrationBuilder.DropTable(
                name: "Erp_InventoryFlow");

            migrationBuilder.DropTable(
                name: "Erp_PaymentAccount");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseAccount");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseGoods");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseReturnAccount");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseReturnGoods");

            migrationBuilder.DropTable(
                name: "Erp_SalesAccount");

            migrationBuilder.DropTable(
                name: "Erp_SalesReturnAccount");

            migrationBuilder.DropTable(
                name: "Erp_SalesReturnGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockCheckBatch");

            migrationBuilder.DropTable(
                name: "Erp_StockInRecordGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockOutRecordGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockTransferGoods");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "ModuleButton");

            migrationBuilder.DropTable(
                name: "ModuleColumn");

            migrationBuilder.DropTable(
                name: "ModuleExcelImportConfig");

            migrationBuilder.DropTable(
                name: "ModuleForm");

            migrationBuilder.DropTable(
                name: "PostUser");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "TaskNodeLog");

            migrationBuilder.DropTable(
                name: "WorkFlowLine");

            migrationBuilder.DropTable(
                name: "WorkFlowNode");

            migrationBuilder.DropTable(
                name: "Interface");

            migrationBuilder.DropTable(
                name: "DataItem");

            migrationBuilder.DropTable(
                name: "Erp_AccountTransferRecord");

            migrationBuilder.DropTable(
                name: "Erp_CollectionOrder");

            migrationBuilder.DropTable(
                name: "Erp_PaymentOrder");

            migrationBuilder.DropTable(
                name: "Erp_SalesGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockCheckGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockInGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockInRecord");

            migrationBuilder.DropTable(
                name: "Erp_Batch");

            migrationBuilder.DropTable(
                name: "Erp_StockOutGoods");

            migrationBuilder.DropTable(
                name: "Erp_StockOutRecord");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "TaskNodeProcesser");

            migrationBuilder.DropTable(
                name: "Erp_Account");

            migrationBuilder.DropTable(
                name: "Erp_StockCheckOrder");

            migrationBuilder.DropTable(
                name: "Erp_StockInOrder");

            migrationBuilder.DropTable(
                name: "Erp_Inventory");

            migrationBuilder.DropTable(
                name: "Erp_StockOutOrder");

            migrationBuilder.DropTable(
                name: "TaskNode");

            migrationBuilder.DropTable(
                name: "Erp_SalesReturnOrder");

            migrationBuilder.DropTable(
                name: "Erp_Goods");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseReturnOrder");

            migrationBuilder.DropTable(
                name: "Erp_StockTransferOrder");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Erp_SalesOrder");

            migrationBuilder.DropTable(
                name: "Erp_GoodsCategory");

            migrationBuilder.DropTable(
                name: "Erp_PurchaseOrder");

            migrationBuilder.DropTable(
                name: "WorkFlowDesign");

            migrationBuilder.DropTable(
                name: "Erp_Customer");

            migrationBuilder.DropTable(
                name: "Erp_Suppiler");

            migrationBuilder.DropTable(
                name: "Erp_Warehouse");

            migrationBuilder.DropTable(
                name: "CustomizedForm");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
