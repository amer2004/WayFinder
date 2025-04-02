using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProjectWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_Admin_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Admins");
        }
    }
}
