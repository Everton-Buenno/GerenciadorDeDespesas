using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTon.GerenciadorDeDespesas.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meses",
                columns: table => new
                {
                    MesId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meses", x => x.MesId);
                });

            migrationBuilder.CreateTable(
                name: "TipoDespesa",
                columns: table => new
                {
                    TipoDespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDespesa", x => x.TipoDespesaId);
                });

            migrationBuilder.CreateTable(
                name: "Salarios",
                columns: table => new
                {
                    SalarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarios", x => x.SalarioId);
                    table.ForeignKey(
                        name: "FK_Salarios_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    DespesaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesId = table.Column<int>(type: "int", nullable: false),
                    TipoDespesaId = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.DespesaId);
                    table.ForeignKey(
                        name: "FK_Despesas_Meses_MesId",
                        column: x => x.MesId,
                        principalTable: "Meses",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despesas_TipoDespesa_TipoDespesaId",
                        column: x => x.TipoDespesaId,
                        principalTable: "TipoDespesa",
                        principalColumn: "TipoDespesaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_MesId",
                table: "Despesas",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_TipoDespesaId",
                table: "Despesas",
                column: "TipoDespesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Salarios_MesId",
                table: "Salarios",
                column: "MesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");

            migrationBuilder.DropTable(
                name: "Salarios");

            migrationBuilder.DropTable(
                name: "TipoDespesa");

            migrationBuilder.DropTable(
                name: "Meses");
        }
    }
}
