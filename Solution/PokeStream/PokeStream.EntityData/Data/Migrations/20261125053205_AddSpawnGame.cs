using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStream.EntityData.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSpawnGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProvisional",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SpawnsEnabled",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProvisional",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SpawnsEnabled",
                table: "Users");
        }
    }
}
