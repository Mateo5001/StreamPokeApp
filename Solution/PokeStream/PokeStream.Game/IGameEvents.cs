namespace PokeStream.Game;

/// <summary>The wild Pokémon of a channel, as something on screen needs it.</summary>
public record WildPokemon(string SpeciesName, int Level, bool IsShiny, string? SpriteUrl, DateTime ExpiresAt);

/// <summary>
/// Tells the outside world what happens in a channel. The host implements it; without one nothing is told.
/// A bot running in its own process would implement it by calling the API.
/// </summary>
public interface IGameEvents
{
    Task SpawnedAsync(Guid streamerId, WildPokemon wild);
    Task CapturedAsync(Guid streamerId, string speciesName, string trainerName);
    Task FledAsync(Guid streamerId, string speciesName);
}

internal sealed class NoGameEvents : IGameEvents
{
    public Task SpawnedAsync(Guid streamerId, WildPokemon wild) => Task.CompletedTask;
    public Task CapturedAsync(Guid streamerId, string speciesName, string trainerName) => Task.CompletedTask;
    public Task FledAsync(Guid streamerId, string speciesName) => Task.CompletedTask;
}
