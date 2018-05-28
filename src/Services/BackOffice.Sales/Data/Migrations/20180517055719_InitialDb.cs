using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BackOffice.Sales.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateSequence(
                name: "SEQ_Categories",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "SEQ_Products",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    Featured = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "sales",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "sales",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "sales");

            migrationBuilder.DropSequence(
                name: "SEQ_Categories");

            migrationBuilder.DropSequence(
                name: "SEQ_Products");
        }
    }
}
