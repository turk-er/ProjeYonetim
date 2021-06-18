using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjeYonetim.Data.Migrations
{
    public partial class bir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departmans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanAdi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjeAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BasTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjeGeliri = table.Column<int>(type: "int", nullable: false),
                    NetGelir = table.Column<int>(type: "int", nullable: false),
                    EkAlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Personels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cinsiyet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Maas = table.Column<double>(type: "float", nullable: false),
                    EkAlan = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ProfilResmi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeparmanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Personels_Departmans_DeparmanID",
                        column: x => x.DeparmanID,
                        principalTable: "Departmans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelProjes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    ProjeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelProjes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonelProjes_Personels_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelProjes_Projes_ProjeID",
                        column: x => x.ProjeID,
                        principalTable: "Projes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YapilacaklarListesis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gorev = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BaslamaZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisZamanı = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Oncelik = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EkAlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YapilacaklarListesis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YapilacaklarListesis_Personels_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonelProjes_PersonelID",
                table: "PersonelProjes",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelProjes_ProjeID",
                table: "PersonelProjes",
                column: "ProjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Personels_DeparmanID",
                table: "Personels",
                column: "DeparmanID");

            migrationBuilder.CreateIndex(
                name: "IX_YapilacaklarListesis_PersonelID",
                table: "YapilacaklarListesis",
                column: "PersonelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelProjes");

            migrationBuilder.DropTable(
                name: "YapilacaklarListesis");

            migrationBuilder.DropTable(
                name: "Projes");

            migrationBuilder.DropTable(
                name: "Personels");

            migrationBuilder.DropTable(
                name: "Departmans");
        }
    }
}
