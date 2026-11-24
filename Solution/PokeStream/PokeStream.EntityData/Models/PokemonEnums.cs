namespace PokeStream.EntityData.Models;

/// <summary>The 25 Pokémon natures (stored by name).</summary>
public enum Nature
{
    Hardy, Lonely, Brave, Adamant, Naughty,
    Bold, Docile, Relaxed, Impish, Lax,
    Timid, Hasty, Serious, Jolly, Naive,
    Modest, Mild, Quiet, Bashful, Rash,
    Calm, Gentle, Sassy, Careful, Quirky
}

public enum PokemonGender
{
    Male,
    Female,
    Genderless
}

/// <summary>Where a capture was made from.</summary>
public enum CapturePlatform
{
    Twitch,
    YouTube,
    Kick,
    Web
}
