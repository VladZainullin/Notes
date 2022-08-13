using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notes.Web.Migrations
{
    public partial class Добавилсостояниезаметки : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteStateId",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoteStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteStateId",
                table: "Notes",
                column: "NoteStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteStates_NoteStateId",
                table: "Notes",
                column: "NoteStateId",
                principalTable: "NoteStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteStates_NoteStateId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "NoteStates");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NoteStateId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteStateId",
                table: "Notes");
        }
    }
}
