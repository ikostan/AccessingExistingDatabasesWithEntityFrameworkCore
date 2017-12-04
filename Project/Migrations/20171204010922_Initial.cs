using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cmp_LastFirst = table.Column<string>(type: "varchar(102)", nullable: true, computedColumnSql: "(([str_fld_LastName]+', ')+[str_fld_FirstName])"),
                    str_fld_Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Email = table.Column<string>(type: "varchar(250)", nullable: true),
                    str_fld_FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_State = table.Column<string>(type: "varchar(50)", nullable: true),
                    str_fld_Zipcode = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<string>(maxLength: 10, nullable: false),
                    ExpirationDays = table.Column<int>(nullable: true),
                    Perishable = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    ProductName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Refrigerated = table.Column<bool>(nullable: true),
                    Size = table.Column<int>(nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", nullable: true),
                    Variety = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SalesGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salesperson",
                columns: table => new
                {
                    SalespersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    SalesGroupState = table.Column<string>(maxLength: 2, nullable: false, defaultValueSql: "(N'CA')"),
                    SalesGroupType = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    State = table.Column<string>(type: "varchar(50)", nullable: true),
                    Zipcode = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salesperson", x => x.SalespersonID);
                    table.ForeignKey(
                        name: "FK_Salesperson_Salesperson",
                        column: x => x.SalespersonID,
                        principalTable: "Salesperson",
                        principalColumn: "SalespersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CustomerID = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<byte[]>(type: "timestamp", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SalespersonID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false, defaultValueSql: "('none')"),
                    TotalDue = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Salesperson",
                        column: x => x.SalespersonID,
                        principalTable: "Salesperson",
                        principalColumn: "SalespersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<string>(maxLength: 10, nullable: false),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product1",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Order",
                table: "Order",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SalespersonID",
                table: "Order",
                column: "SalespersonID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductID",
                table: "OrderItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StateType",
                table: "SalesGroup",
                columns: new[] { "State", "Type" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "SalesGroup");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Salesperson");
        }
    }
}
