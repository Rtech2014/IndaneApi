using Microsoft.EntityFrameworkCore.Migrations;

namespace IndaneApi.Data.Migrations
{
    public partial class loading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Empty",
                table: "Loadings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Expense",
                table: "Loadings",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Empty",
                table: "Loadings");

            migrationBuilder.DropColumn(
                name: "Expense",
                table: "Loadings");
        }
    }
}
