using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet_Reu.Migrations
{
    public partial class Projet_ReuModelsReuContextbis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classe",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flight");

            migrationBuilder.AddColumn<string>(
                name: "Classe",
                table: "UserFlightRelations",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UserFlightRelations",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classe",
                table: "UserFlightRelations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserFlightRelations");

            migrationBuilder.AddColumn<string>(
                name: "Classe",
                table: "Flight",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Flight",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
