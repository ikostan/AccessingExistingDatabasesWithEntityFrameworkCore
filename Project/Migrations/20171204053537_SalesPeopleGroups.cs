using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Migrations
{
    public partial class SalesPeopleGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salesperson_Salesperson",
                table: "Salesperson");

            migrationBuilder.AlterColumn<bool>(
                name: "Perishable",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SalesGroup_State_Type",
                table: "SalesGroup",
                columns: new[] { "State", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Salesperson_SalesGroupState_SalesGroupType",
                table: "Salesperson",
                columns: new[] { "SalesGroupState", "SalesGroupType" });

            migrationBuilder.AddForeignKey(
                name: "FK_Salesperson_SalesGroup_SalesGroupState_SalesGroupType",
                table: "Salesperson",
                columns: new[] { "SalesGroupState", "SalesGroupType" },
                principalTable: "SalesGroup",
                principalColumns: new[] { "State", "Type" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salesperson_SalesGroup_SalesGroupState_SalesGroupType",
                table: "Salesperson");

            migrationBuilder.DropIndex(
                name: "IX_Salesperson_SalesGroupState_SalesGroupType",
                table: "Salesperson");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SalesGroup_State_Type",
                table: "SalesGroup");

            migrationBuilder.AlterColumn<bool>(
                name: "Perishable",
                table: "Product",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_Salesperson_Salesperson",
                table: "Salesperson",
                column: "SalespersonID",
                principalTable: "Salesperson",
                principalColumn: "SalespersonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
