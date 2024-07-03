using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorTestes.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBMateria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    Disciplina_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMateria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBMateria_TBDisciplina",
                        column: x => x.Disciplina_Id,
                        principalTable: "TBDisciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBMateria_Disciplina_Id",
                table: "TBMateria",
                column: "Disciplina_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMateria");
        }
    }
}
