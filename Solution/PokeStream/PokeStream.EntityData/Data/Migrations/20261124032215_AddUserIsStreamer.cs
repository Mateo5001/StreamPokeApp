using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStream.EntityData.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIsStreamer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStreamer",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStreamer",
                table: "Users");
        }
    }
}
