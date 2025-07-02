using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteTecnicoEffecti.Migrations
{
    /// <inheritdoc />
    public partial class addindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Universidade",
                table: "Licitacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacoes_CodigoUASG",
                table: "Licitacoes",
                column: "CodigoUASG");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacoes_NumeroPregao",
                table: "Licitacoes",
                column: "NumeroPregao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Licitacoes_CodigoUASG",
                table: "Licitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Licitacoes_NumeroPregao",
                table: "Licitacoes");

            migrationBuilder.AddColumn<string>(
                name: "Universidade",
                table: "Licitacoes",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
