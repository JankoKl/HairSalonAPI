using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FrizerskiSalon.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Izvestaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BrojTermina = table.Column<int>(type: "INTEGER", nullable: false),
                    BrojRazlicitihKlijenata = table.Column<int>(type: "INTEGER", nullable: false),
                    UkupnaZarada = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvestaji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BrTel = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Radnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ime = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    BrTel = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Pozicija = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RadnikID = table.Column<int>(type: "INTEGER", nullable: false),
                    KlijentID = table.Column<int>(type: "INTEGER", nullable: false),
                    UslugaID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termini_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termini_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Termini_Usluge_UslugaID",
                        column: x => x.UslugaID,
                        principalTable: "Usluge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Klijenti",
                columns: new[] { "Id", "BrTel", "Ime", "Prezime" },
                values: new object[,]
                {
                    { 1, "+3816600001", "Miloš", "Milić" },
                    { 2, "+38164000770", "Tatjana", "Konstatinović" }
                });

            migrationBuilder.InsertData(
                table: "Radnici",
                columns: new[] { "Id", "BrTel", "Email", "Ime", "Pozicija", "Prezime" },
                values: new object[,]
                {
                    { 1, "+381641234567", "marko.jovanovic@example.com", "Marko", "Frizer", "Jovanović" },
                    { 2, "+381652345678", "ana.petrović@example.com", "Ana", "Pomoćnik", "Petrović" }
                });

            migrationBuilder.InsertData(
                table: "Usluge",
                columns: new[] { "Id", "Cena", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, 600m, "Šišanje", "Skraćivanje kose" },
                    { 2, 200m, "Brijanje", "Oblikovanje brade i brkova" }
                });

            migrationBuilder.InsertData(
                table: "Termini",
                columns: new[] { "Id", "Datum", "KlijentID", "RadnikID", "Status", "UslugaID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Zakazano", 1 },
                    { 2, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "Zakazano", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Termini_KlijentID",
                table: "Termini",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_RadnikID",
                table: "Termini",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Termini_UslugaID",
                table: "Termini",
                column: "UslugaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izvestaji");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Radnici");

            migrationBuilder.DropTable(
                name: "Usluge");
        }
    }
}
