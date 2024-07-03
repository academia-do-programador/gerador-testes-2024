using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeradorTestes.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBTeste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(250)", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProvaRecuperacao = table.Column<bool>(type: "bit", nullable: false),
                    Disciplina_Id = table.Column<int>(type: "int", nullable: false),
                    Materia_Id = table.Column<int>(type: "int", nullable: true),
                    QuantidadeQuestoes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTeste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBTeste_TBDisciplina",
                        column: x => x.Disciplina_Id,
                        principalTable: "TBDisciplina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBTeste_TBMateria",
                        column: x => x.Materia_Id,
                        principalTable: "TBMateria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBTeste_TBQuestao",
                columns: table => new
                {
                    QuestoesId = table.Column<int>(type: "int", nullable: false),
                    TesteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTeste_TBQuestao", x => new { x.QuestoesId, x.TesteId });
                    table.ForeignKey(
                        name: "FK_TBTeste_TBQuestao_TBQuestao_QuestoesId",
                        column: x => x.QuestoesId,
                        principalTable: "TBQuestao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBTeste_TBQuestao_TBTeste_TesteId",
                        column: x => x.TesteId,
                        principalTable: "TBTeste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBTeste_Disciplina_Id",
                table: "TBTeste",
                column: "Disciplina_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBTeste_Materia_Id",
                table: "TBTeste",
                column: "Materia_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TBTeste_TBQuestao_TesteId",
                table: "TBTeste_TBQuestao",
                column: "TesteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBTeste_TBQuestao");

            migrationBuilder.DropTable(
                name: "TBTeste");
        }
    }
}
