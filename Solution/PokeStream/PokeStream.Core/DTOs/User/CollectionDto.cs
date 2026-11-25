namespace PokeStream.Core.DTOs.User;

public record TeamMemberDto(string Name, int Level, bool IsShiny, string? SpriteUrl);

/// <summary>The signed-in user's Pokémon: national Pokédex numbers owned, and the team.</summary>
public record CollectionDto(IReadOnlyList<int> SpeciesIds, IReadOnlyList<TeamMemberDto> Team);
