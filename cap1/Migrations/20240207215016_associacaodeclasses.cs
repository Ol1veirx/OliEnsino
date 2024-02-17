using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cap1.Migrations
{
    /// <inheritdoc />
    public partial class associacaodeclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstituicaoID",
                table: "Departamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_InstituicaoID",
                table: "Departamentos",
                column: "InstituicaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos",
                column: "InstituicaoID",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_Instituicoes_InstituicaoID",
                table: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_InstituicaoID",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "InstituicaoID",
                table: "Departamentos");
        }
    }
}
