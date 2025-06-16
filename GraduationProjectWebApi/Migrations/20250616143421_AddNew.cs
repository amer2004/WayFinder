using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookDate",
                table: "HotelReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AdminId",
                table: "Flights",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Admins_AdminId",
                table: "Flights",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Admins_AdminId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_AdminId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BookDate",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Flights");
        }
    }
}
