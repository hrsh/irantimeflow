﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace IranTimeFlow.WebApp.Migrations
{
    public partial class AddPublishedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Timelines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Timelines");
        }
    }
}
