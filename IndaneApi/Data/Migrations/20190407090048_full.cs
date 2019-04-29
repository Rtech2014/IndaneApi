using Microsoft.EntityFrameworkCore.Migrations;

namespace IndaneApi.Data.Migrations
{
    public partial class full : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CashToBeRecevied",
                table: "Fulls",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashToBeRecevied",
                table: "Fulls");
        }
    }
}
