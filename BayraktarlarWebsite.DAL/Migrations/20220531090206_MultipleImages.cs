using Microsoft.EntityFrameworkCore.Migrations;

namespace BayraktarlarWebsite.DAL.Migrations
{
    public partial class MultipleImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Tabelas");

            migrationBuilder.CreateTable(
                name: "TabelaImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TabelaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaImages_Tabelas_TabelaId",
                        column: x => x.TabelaId,
                        principalTable: "Tabelas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaImages_TabelaId",
                table: "TabelaImages",
                column: "TabelaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaImages");

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "Tabelas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
