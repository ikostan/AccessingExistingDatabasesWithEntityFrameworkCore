using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Migrations.SQLiteDB
{
    public partial class InitialSQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerSQLite",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    str_fld_Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Email = table.Column<string>(type: "varchar(300)", nullable: true),
                    str_fld_FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_State = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Zipcode = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSQLite", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSQLite",
                columns: table => new
                {
                    ExpirationDays = table.Column<int>(nullable: true),
                    Refrigerated = table.Column<bool>(nullable: true),
                    ProductID = table.Column<string>(maxLength: 10, nullable: false),
                    Perishable = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    ProductName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Size = table.Column<int>(nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", nullable: true),
                    Variety = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSQLite", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SalesGroupSQLite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesGroupSQLite", x => x.Id);
                    table.UniqueConstraint("AK_SalesGroupSQLite_State_Type", x => new { x.State, x.Type });
                });

            migrationBuilder.CreateTable(
                name: "SalespersonSQLite",
                columns: table => new
                {
                    SalespersonID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    SalesGroupSQLiteState = table.Column<string>(nullable: true),
                    SalesGroupSQLiteType = table.Column<int>(nullable: true),
                    SalesGroupState = table.Column<string>(maxLength: 2, nullable: false, defaultValue: "CA"),
                    SalesGroupType = table.Column<int>(nullable: false, defaultValueSql: "1")
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "varchar(50)", nullable: true),
                    Zipcode = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalespersonSQLite", x => x.SalespersonID);
                    table.ForeignKey(
                        name: "FK_SalespersonSQLite_SalesGroupSQLite_SalesGroupSQLiteState_SalesGroupSQLiteType",
                        columns: x => new { x.SalesGroupSQLiteState, x.SalesGroupSQLiteType },
                        principalTable: "SalesGroupSQLite",
                        principalColumns: new[] { "State", "Type" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSQLite",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CustomerID = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SalespersonID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false, defaultValueSql: "('none')"),
                    TotalDue = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSQLite", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerID,
                        principalTable: "CustomerSQLite",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Salesperson",
                        column: x => x.SalespersonID,
                        principalTable: "SalespersonSQLite",
                        principalColumn: "SalespersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemSQLite",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<string>(maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemSQLite", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderID,
                        principalTable: "OrderSQLite",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product1",
                        column: x => x.ProductID,
                        principalTable: "ProductSQLite",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSQLite_str_fld_LastName",
                table: "CustomerSQLite",
                column: "str_fld_LastName");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSQLite_OrderID",
                table: "OrderItemSQLite",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSQLite_ProductID",
                table: "OrderItemSQLite",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSQLite_CustomerID",
                table: "OrderSQLite",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order",
                table: "OrderSQLite",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSQLite_SalespersonID",
                table: "OrderSQLite",
                column: "SalespersonID");

            migrationBuilder.CreateIndex(
                name: "IX_StateType",
                table: "SalesGroupSQLite",
                columns: new[] { "State", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalespersonSQLite_SalesGroupSQLiteState_SalesGroupSQLiteType",
                table: "SalespersonSQLite",
                columns: new[] { "SalesGroupSQLiteState", "SalesGroupSQLiteType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemSQLite");

            migrationBuilder.DropTable(
                name: "OrderSQLite");

            migrationBuilder.DropTable(
                name: "ProductSQLite");

            migrationBuilder.DropTable(
                name: "CustomerSQLite");

            migrationBuilder.DropTable(
                name: "SalespersonSQLite");

            migrationBuilder.DropTable(
                name: "SalesGroupSQLite");
        }
    }
}
