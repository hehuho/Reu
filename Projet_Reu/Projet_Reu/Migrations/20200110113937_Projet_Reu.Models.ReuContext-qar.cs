using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet_Reu.Migrations
{
    public partial class Projet_ReuModelsReuContextqar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classe",
                table: "UserFlightRelations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserFlightRelations");

            migrationBuilder.DropColumn(
                name: "NbSiege",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "UserFlightRelations",
                newName: "ClasseId");

            migrationBuilder.AddColumn<string>(
                name: "Arrivee",
                table: "Flight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Depart",
                table: "Flight",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Arrivee",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "Depart",
                table: "Flight");

            migrationBuilder.RenameColumn(
                name: "ClasseId",
                table: "UserFlightRelations",
                newName: "FlightId");

            migrationBuilder.AddColumn<string>(
                name: "Classe",
                table: "UserFlightRelations",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UserFlightRelations",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NbSiege",
                table: "Flight",
                nullable: false,
                defaultValue: 0);
        }
    }
}
