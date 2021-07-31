using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace controleDePontoV1.Migrations
{
    public partial class controlePonto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "colaboradores",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sobreNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    papel = table.Column<int>(type: "int", nullable: false),
                    equipe = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "controleApontamento",
                columns: table => new
                {
                    codigo_Projeto = table.Column<int>(type: "int", nullable: false),
                    codigo_Equipe = table.Column<int>(type: "int", nullable: false),
                    codigo_Colaborador = table.Column<int>(type: "int", nullable: false),
                    dateTime = table.Column<DateTime>(type: "dateTime", nullable: false),
                    dia_Fim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controleApontamento", x => new { x.codigo_Colaborador, x.codigo_Equipe, x.codigo_Projeto, x.dateTime });
                });

            migrationBuilder.CreateTable(
                name: "equipes",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipes", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "histEquipes",
                columns: table => new
                {
                    codigo_Colaborador = table.Column<int>(type: "int", nullable: false),
                    codigo_Equipe = table.Column<int>(type: "int", nullable: false),
                    dataDaAlteracao = table.Column<DateTime>(type: "dateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_histEquipes", x => new { x.codigo_Colaborador, x.codigo_Equipe });
                });

            migrationBuilder.CreateTable(
                name: "papeis",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_papeis", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "projetos",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false),
                    descrição = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projetos", x => x.codigo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colaboradores");

            migrationBuilder.DropTable(
                name: "controleApontamento");

            migrationBuilder.DropTable(
                name: "equipes");

            migrationBuilder.DropTable(
                name: "histEquipes");

            migrationBuilder.DropTable(
                name: "papeis");

            migrationBuilder.DropTable(
                name: "projetos");
        }
    }
}
