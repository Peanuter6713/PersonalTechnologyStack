using Microsoft.EntityFrameworkCore.Migrations;

namespace efcoredemo.Migrations
{
    public partial class RevertName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "T_Persons");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "T_Persons",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_Persons",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "T_Persons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
