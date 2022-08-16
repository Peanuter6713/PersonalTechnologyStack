using Microsoft.EntityFrameworkCore.Migrations;

namespace efcoredemo.Migrations
{
    public partial class AddTwoNameToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Books",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "T_Persons");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "T_Persons",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
