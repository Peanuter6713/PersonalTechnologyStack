using Microsoft.EntityFrameworkCore.Migrations;

namespace efcoredemo.Migrations
{
    public partial class ChangeTitleToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "T_Books",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "T_Books",
                newName: "Title");
        }
    }
}
