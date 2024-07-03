using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorTestes.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBQuestao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Enunciado = table.Column<string>(type: "varchar(500)", nullable: false),
                    Materia_Id = table.Column<int>(type: "int", nullable: false),
                    UtilizadaEmTeste = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBQuestao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBQuestao_TBMateria",
                        column: x => x.Materia_Id,
                        principalTable: "TBMateria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBAlternativa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Letra = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Resposta = table.Column<string>(type: "varchar(100)", nullable: false),
                    Correta = table.Column<bool>(type: "bit", nullable: false),
                    Questao_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAlternativa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBAlternativa_TBQuestao",
                        column: x => x.Questao_Id,
                        principalTable: "TBQuestao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAlternativa_Questao_Id",
                table: "TBAlternativa",
                column: "Questao_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBQuestao_Materia_Id",
                table: "TBQuestao",
                column: "Materia_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAlternativa");

            migrationBuilder.DropTable(
                name: "TBQuestao");
        }
    }
}
