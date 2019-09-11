using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesRecordImport.DataAccess.EFCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Region = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    SalesChannel = table.Column<int>(nullable: false),
                    OrderPriority = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ExternalId = table.Column<long>(nullable: false),
                    ShipDate = table.Column<DateTime>(nullable: false),
                    UnitsSold = table.Column<decimal>(type: "money", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    UnitCost = table.Column<decimal>(type: "money", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "money", nullable: false),
                    TotalCost = table.Column<decimal>(type: "money", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_Country",
                table: "SalesRecords",
                column: "Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesRecords");
        }
    }
}
