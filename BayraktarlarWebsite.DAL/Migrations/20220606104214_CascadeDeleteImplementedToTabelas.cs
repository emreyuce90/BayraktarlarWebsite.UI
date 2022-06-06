using Microsoft.EntityFrameworkCore.Migrations;

namespace BayraktarlarWebsite.DAL.Migrations
{
    public partial class CascadeDeleteImplementedToTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaImages_Tabelas_TabelaId",
                table: "TabelaImages");

            migrationBuilder.AlterColumn<int>(
                name: "TabelaId",
                table: "TabelaImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaImages_Tabelas_TabelaId",
                table: "TabelaImages",
                column: "TabelaId",
                principalTable: "Tabelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabelaImages_Tabelas_TabelaId",
                table: "TabelaImages");

            migrationBuilder.AlterColumn<int>(
                name: "TabelaId",
                table: "TabelaImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TabelaImages_Tabelas_TabelaId",
                table: "TabelaImages",
                column: "TabelaId",
                principalTable: "Tabelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
