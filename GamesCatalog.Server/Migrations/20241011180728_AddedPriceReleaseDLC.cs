using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesCatalog.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriceReleaseDLC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentGameId",
                table: "Games",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Games",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ParentGameId",
                table: "Games",
                column: "ParentGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Games_ParentGameId",
                table: "Games",
                column: "ParentGameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Games_ParentGameId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ParentGameId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ParentGameId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Games");
        }
    }
}
