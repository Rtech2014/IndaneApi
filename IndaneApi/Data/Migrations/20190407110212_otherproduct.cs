using Microsoft.EntityFrameworkCore.Migrations;

namespace IndaneApi.Data.Migrations
{
    public partial class otherproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Income",
                table: "OtherProductSales",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Expense",
                table: "OtherProductLoads",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Income",
                table: "OtherProductSales");

            migrationBuilder.DropColumn(
                name: "Expense",
                table: "OtherProductLoads");
        }
    }
}
