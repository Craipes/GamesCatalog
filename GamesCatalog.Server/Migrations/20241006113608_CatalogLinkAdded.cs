using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesCatalog.Server.Migrations
{
    /// <inheritdoc />
    public partial class CatalogLinkAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogGame");

            migrationBuilder.CreateTable(
                name: "CatalogLinks",
                columns: table => new
                {
                    CatalogId = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogLinks", x => new { x.CatalogId, x.GameId });
                    table.ForeignKey(
                        name: "FK_CatalogLinks_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogLinks_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogLinks_GameId",
                table: "CatalogLinks",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogLinks");

            migrationBuilder.CreateTable(
                name: "CatalogGame",
                columns: table => new
                {
                    CatalogsId = table.Column<int>(type: "INTEGER", nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogGame", x => new { x.CatalogsId, x.GameId });
                    table.ForeignKey(
                        name: "FK_CatalogGame_Catalogs_CatalogsId",
                        column: x => x.CatalogsId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogGame_GameId",
                table: "CatalogGame",
                column: "GameId");
        }
    }
}
