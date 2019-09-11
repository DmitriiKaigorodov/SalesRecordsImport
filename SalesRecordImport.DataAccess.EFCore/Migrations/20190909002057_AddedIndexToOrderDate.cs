using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesRecordImport.DataAccess.EFCore.Migrations
{
    public partial class AddedIndexToOrderDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesRecords_OrderDate",
                table: "SalesRecords",
                column: "OrderDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesRecords_OrderDate",
                table: "SalesRecords");
        }
    }
}
