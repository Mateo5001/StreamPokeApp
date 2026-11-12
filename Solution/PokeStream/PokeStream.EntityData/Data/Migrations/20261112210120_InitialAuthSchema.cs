using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStream.EntityData.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialAuthSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActiveAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TwitchUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitchUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    YouTubeChannelId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    YouTubeUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KickUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KickUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiscordUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiscordUsername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TwitchAccessToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TwitchRefreshToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TwitchTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YouTubeAccessToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    YouTubeRefreshToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    YouTubeTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KickAccessToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    KickRefreshToken = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    KickTokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DiscordUserId",
                table: "Users",
                column: "DiscordUserId",
                unique: true,
                filter: "[DiscordUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_KickUserId",
                table: "Users",
                column: "KickUserId",
                unique: true,
                filter: "[KickUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TwitchUserId",
                table: "Users",
                column: "TwitchUserId",
                unique: true,
                filter: "[TwitchUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_YouTubeChannelId",
                table: "Users",
                column: "YouTubeChannelId",
                unique: true,
                filter: "[YouTubeChannelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_ExpiresAt",
                table: "UserSessions",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_IsActive",
                table: "UserSessions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_RefreshToken",
                table: "UserSessions",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
