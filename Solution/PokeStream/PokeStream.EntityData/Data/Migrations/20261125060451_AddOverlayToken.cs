using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStream.EntityData.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOverlayToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OverlayToken",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OverlayToken",
                table: "Users",
                column: "OverlayToken",
                unique: true,
                filter: "[OverlayToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_OverlayToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OverlayToken",
                table: "Users");
        }
    }
}
