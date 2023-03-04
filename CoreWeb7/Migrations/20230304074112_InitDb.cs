using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoreWeb7.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<int>(type: "int", nullable: false),
                    Adi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    GobekAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Soyadi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EPosta = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogretmenler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    GobekAdi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Soyadi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    EPosta = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogretmenler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DersOgrenci",
                columns: table => new
                {
                    DerslerId = table.Column<int>(type: "int", nullable: false),
                    OgrencilerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersOgrenci", x => new { x.DerslerId, x.OgrencilerId });
                    table.ForeignKey(
                        name: "FK_DersOgrenci_Dersler_DerslerId",
                        column: x => x.DerslerId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersOgrenci_Ogrenciler_OgrencilerId",
                        column: x => x.OgrencilerId,
                        principalTable: "Ogrenciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolumler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgretmenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolumler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolumler_Ogretmenler_OgretmenId",
                        column: x => x.OgretmenId,
                        principalTable: "Ogretmenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DersOgretmen",
                columns: table => new
                {
                    DerslerId = table.Column<int>(type: "int", nullable: false),
                    OgretmenlerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersOgretmen", x => new { x.DerslerId, x.OgretmenlerId });
                    table.ForeignKey(
                        name: "FK_DersOgretmen_Dersler_DerslerId",
                        column: x => x.DerslerId,
                        principalTable: "Dersler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersOgretmen_Ogretmenler_OgretmenlerId",
                        column: x => x.OgretmenlerId,
                        principalTable: "Ogretmenler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "Id", "Adi" },
                values: new object[,]
                {
                    { 1, "JS" },
                    { 2, "Sql" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolumler_OgretmenId",
                table: "Bolumler",
                column: "OgretmenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_Adi",
                table: "Dersler",
                column: "Adi",
                unique: true,
                filter: "[Adi] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DersOgrenci_OgrencilerId",
                table: "DersOgrenci",
                column: "OgrencilerId");

            migrationBuilder.CreateIndex(
                name: "IX_DersOgretmen_OgretmenlerId",
                table: "DersOgretmen",
                column: "OgretmenlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_Adi_Soyadi",
                table: "Ogrenciler",
                columns: new[] { "Adi", "Soyadi" },
                unique: true,
                filter: "[Adi] IS NOT NULL AND [Soyadi] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bolumler");

            migrationBuilder.DropTable(
                name: "DersOgrenci");

            migrationBuilder.DropTable(
                name: "DersOgretmen");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Ogretmenler");
        }
    }
}
