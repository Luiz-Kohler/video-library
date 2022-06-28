using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cpf = table.Column<string>(type: "VARCHAR(11)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_nascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    criado_em = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ultima_atualizacao_em = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    classificacao = table.Column<int>(type: "int", nullable: false),
                    lancamento = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    criado_em = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ultima_atualizacao_em = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filmes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "locacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data_locacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataPrazoDevolucao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_devolucao = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    FilmeId = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ultima_atualizacao_em = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_locacoes_clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locacoes_filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_cpf_ativo",
                table: "clientes",
                columns: new[] { "cpf", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_id_ativo",
                table: "clientes",
                columns: new[] { "Id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_nome_ativo",
                table: "clientes",
                columns: new[] { "nome", "ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_filmes_Id_ativo",
                table: "filmes",
                columns: new[] { "Id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_filmes_lancamento_ativo",
                table: "filmes",
                columns: new[] { "lancamento", "ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_filmes_titulo_ativo",
                table: "filmes",
                columns: new[] { "titulo", "ativo" });

            migrationBuilder.CreateIndex(
                name: "ix_id_ativo1",
                table: "locacoes",
                columns: new[] { "Id", "ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_locacoes_ClienteId",
                table: "locacoes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_locacoes_FilmeId",
                table: "locacoes",
                column: "FilmeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locacoes");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "filmes");
        }
    }
}
