using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeStream.EntityData.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeciesId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Nature = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsShiny = table.Column<bool>(type: "bit", nullable: false),
                    AbilityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsHiddenAbility = table.Column<bool>(type: "bit", nullable: false),
                    IvHp = table.Column<byte>(type: "tinyint", nullable: false),
                    IvAttack = table.Column<byte>(type: "tinyint", nullable: false),
                    IvDefense = table.Column<byte>(type: "tinyint", nullable: false),
                    IvSpecialAttack = table.Column<byte>(type: "tinyint", nullable: false),
                    IvSpecialDefense = table.Column<byte>(type: "tinyint", nullable: false),
                    IvSpeed = table.Column<byte>(type: "tinyint", nullable: false),
                    EvHp = table.Column<byte>(type: "tinyint", nullable: false),
                    EvAttack = table.Column<byte>(type: "tinyint", nullable: false),
                    EvDefense = table.Column<byte>(type: "tinyint", nullable: false),
                    EvSpecialAttack = table.Column<byte>(type: "tinyint", nullable: false),
                    EvSpecialDefense = table.Column<byte>(type: "tinyint", nullable: false),
                    EvSpeed = table.Column<byte>(type: "tinyint", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginStreamerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpawnedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CapturedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CapturedFrom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.CheckConstraint("CK_Pokemons_Evs", "[EvHp] <= 252 AND [EvAttack] <= 252 AND [EvDefense] <= 252 AND [EvSpecialAttack] <= 252 AND [EvSpecialDefense] <= 252 AND [EvSpeed] <= 252 AND [EvHp] + [EvAttack] + [EvDefense] + [EvSpecialAttack] + [EvSpecialDefense] + [EvSpeed] <= 510");
                    table.CheckConstraint("CK_Pokemons_Ivs", "[IvHp] <= 31 AND [IvAttack] <= 31 AND [IvDefense] <= 31 AND [IvSpecialAttack] <= 31 AND [IvSpecialDefense] <= 31 AND [IvSpeed] <= 31");
                    table.CheckConstraint("CK_Pokemons_Level", "[Level] BETWEEN 1 AND 100");
                    table.ForeignKey(
                        name: "FK_Pokemons_Users_OriginStreamerId",
                        column: x => x.OriginStreamerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PartySlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slot = table.Column<byte>(type: "tinyint", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartySlots", x => x.Id);
                    table.CheckConstraint("CK_PartySlots_Slot", "[Slot] BETWEEN 1 AND 6");
                    table.ForeignKey(
                        name: "FK_PartySlots_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PartySlots_Users_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slot = table.Column<byte>(type: "tinyint", nullable: false),
                    MoveName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMoves", x => x.Id);
                    table.CheckConstraint("CK_PokemonMoves_Slot", "[Slot] BETWEEN 1 AND 4");
                    table.ForeignKey(
                        name: "FK_PokemonMoves_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartySlots_PokemonId",
                table: "PartySlots",
                column: "PokemonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartySlots_TrainerId_Slot",
                table: "PartySlots",
                columns: new[] { "TrainerId", "Slot" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_PokemonId_MoveName",
                table: "PokemonMoves",
                columns: new[] { "PokemonId", "MoveName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonMoves_PokemonId_Slot",
                table: "PokemonMoves",
                columns: new[] { "PokemonId", "Slot" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_OriginStreamerId_ExpiresAt",
                table: "Pokemons",
                columns: new[] { "OriginStreamerId", "ExpiresAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_SpeciesId",
                table: "Pokemons",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_TrainerId",
                table: "Pokemons",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_TrainerId_SpeciesId",
                table: "Pokemons",
                columns: new[] { "TrainerId", "SpeciesId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartySlots");

            migrationBuilder.DropTable(
                name: "PokemonMoves");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
