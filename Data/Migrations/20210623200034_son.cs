using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeYonetim.Data.Migrations
{
    public partial class son : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NetKar",
                table: "Projes",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetKar",
                table: "Projes");
        }
    }
}
