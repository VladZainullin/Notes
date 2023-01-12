using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Web.Migrations
{
    public partial class Изменяеттипвремени : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfInclusion",
                table: "Reminder");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeOfInclusion",
                table: "Reminder",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOfInclusion",
                table: "Reminder");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfInclusion",
                table: "Reminder",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
