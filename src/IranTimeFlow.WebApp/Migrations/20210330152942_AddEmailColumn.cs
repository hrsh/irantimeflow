using Microsoft.EntityFrameworkCore.Migrations;

namespace IranTimeFlow.WebApp.Migrations
{
    public partial class AddEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmail",
                table: "Timelines",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByEmail",
                table: "Timelines");
        }
    }
}
