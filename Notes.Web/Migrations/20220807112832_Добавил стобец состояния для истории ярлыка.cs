using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Web.Migrations
{
    public partial class Добавилстобецсостояниядляисторииярлыка : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfModification",
                table: "LabelHistory",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "LabelHistory",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "LabelHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfModification",
                table: "LabelHistory",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
