using Microsoft.EntityFrameworkCore.Migrations;

namespace BayraktarlarWebsite.DAL.Migrations
{
    public partial class mig_17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
               name: "IsAssigned",
               table: "Tickets",
               type: "bit",
               nullable: true);
              
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
