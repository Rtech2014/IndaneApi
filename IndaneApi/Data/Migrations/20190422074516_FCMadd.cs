using Microsoft.EntityFrameworkCore.Migrations;

namespace IndaneApi.Data.Migrations
{
    public partial class FCMadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CashToBeRecevied",
                table: "Empties",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FullId",
                table: "Empties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ReceviedBalance",
                table: "Empties",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Empties_FullId",
                table: "Empties",
                column: "FullId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empties_Fulls_FullId",
                table: "Empties",
                column: "FullId",
                principalTable: "Fulls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empties_Fulls_FullId",
                table: "Empties");

            migrationBuilder.DropIndex(
                name: "IX_Empties_FullId",
                table: "Empties");

            migrationBuilder.DropColumn(
                name: "CashToBeRecevied",
                table: "Empties");

            migrationBuilder.DropColumn(
                name: "FullId",
                table: "Empties");

            migrationBuilder.DropColumn(
                name: "ReceviedBalance",
                table: "Empties");
        }
    }
}
