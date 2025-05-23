using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectWebApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Guides",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nationality",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image4",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Guides",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Guides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nationality",
                table: "Guides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AirLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Image4",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AirLines");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Guides",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Admins",
                newName: "Name");
        }
    }
}
