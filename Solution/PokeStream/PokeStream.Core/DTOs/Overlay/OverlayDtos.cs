namespace PokeStream.Core.DTOs.Overlay;

/// <summary>The wild Pokémon as the overlay shows it. Seconds, not a date, so the viewer's clock does not matter.</summary>
public record OverlayWild(string SpeciesName, int Level, bool IsShiny, string? SpriteUrl, int SecondsLeft);

/// <summary>Secret token of a streamer's overlay URL.</summary>
public record OverlayTokenResponse(string Token);

/// <summary>Names shared by the overlay hub and its client.</summary>
public static class OverlayHubContract
{
    public const string Path = "hubs/overlay";

    /// <summary>Client → server: join(token), returns the active <see cref="OverlayWild"/> or null.</summary>
    public const string Join = "Join";

    // Server → client
    public const string Spawned = "Spawned";   // OverlayWild
    public const string Captured = "Captured"; // speciesName, trainerName
    public const string Fled = "Fled";         // speciesName
}
