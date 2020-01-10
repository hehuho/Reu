using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet_Reu.Migrations
{
    public partial class Projet_ReuModelsReuContextter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "UserFlightRelations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "UserFlightRelations");
        }
    }
}
