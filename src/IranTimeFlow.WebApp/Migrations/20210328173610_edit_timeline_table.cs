using Microsoft.EntityFrameworkCore.Migrations;

namespace IranTimeFlow.WebApp.Migrations
{
    public partial class edit_timeline_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Timelines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "Timelines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Timelines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Timelines");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Timelines");
        }
    }
}
