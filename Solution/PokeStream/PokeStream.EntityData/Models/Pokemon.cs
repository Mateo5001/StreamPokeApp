using System.ComponentModel.DataAnnotations;

namespace PokeStream.EntityData.Models;

/// <summary>
/// One unique Pokémon individual. Species data (name, types, sprites, base stats, move/ability details)
/// is NOT mirrored here: it lives in PokeAPI and is referenced by <see cref="SpeciesId"/>.
/// A Pokémon is created "wild" when it appears on a stream (<see cref="TrainerId"/> is null)
/// and becomes owned when a viewer captures it.
/// </summary>
public class Pokemon
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>PokeAPI species id (national Pokédex number).</summary>
    public int SpeciesId { get; set; }

    [MaxLength(50)]
    public string? Nickname { get; set; }

    // Characteristics
    public byte Level { get; set; } = 1;
    public int Experience { get; set; }
    public Nature Nature { get; set; }
    public PokemonGender Gender { get; set; }
    public bool IsShiny { get; set; }

    /// <summary>PokeAPI ability slug (e.g. "static").</summary>
    [Required]
    [MaxLength(100)]
    public string AbilityName { get; set; } = string.Empty;
    public bool IsHiddenAbility { get; set; }

    // Individual values (0-31), fixed at spawn
    public byte IvHp { get; set; }
    public byte IvAttack { get; set; }
    public byte IvDefense { get; set; }
    public byte IvSpecialAttack { get; set; }
    public byte IvSpecialDefense { get; set; }
    public byte IvSpeed { get; set; }

    // Effort values (0-252 each, 510 total), grow with training
    public byte EvHp { get; set; }
    public byte EvAttack { get; set; }
    public byte EvDefense { get; set; }
    public byte EvSpecialAttack { get; set; }
    public byte EvSpecialDefense { get; set; }
    public byte EvSpeed { get; set; }

    // Ownership: null while wild
    public Guid? TrainerId { get; set; }
    public virtual User? Trainer { get; set; }

    // Origin
    /// <summary>Streamer whose channel it appeared in.</summary>
    public Guid? OriginStreamerId { get; set; }
    public virtual User? OriginStreamer { get; set; }
    public DateTime SpawnedAt { get; set; } = DateTime.UtcNow;
    /// <summary>When a wild Pokémon flees if nobody captured it.</summary>
    public DateTime? ExpiresAt { get; set; }
    public DateTime? CapturedAt { get; set; }
    public CapturePlatform? CapturedFrom { get; set; }

    /// <summary>Optimistic concurrency token: two viewers capturing the same Pokémon cannot both win.</summary>
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<PokemonMove> Moves { get; set; } = new List<PokemonMove>();
}
