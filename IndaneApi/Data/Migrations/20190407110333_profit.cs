using Microsoft.EntityFrameworkCore.Migrations;

namespace IndaneApi.Data.Migrations
{
    public partial class profit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Profit",
                table: "OtherProductSales",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profit",
                table: "OtherProductSales");
        }
    }
}
