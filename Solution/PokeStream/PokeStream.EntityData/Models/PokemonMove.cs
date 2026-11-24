using System.ComponentModel.DataAnnotations;

namespace PokeStream.EntityData.Models;

/// <summary>A move known by a Pokémon (max 4, slot 1-4). Move details come from PokeAPI.</summary>
public class PokemonMove
{
    [Key]
    public int Id { get; set; }

    public Guid PokemonId { get; set; }
    public virtual Pokemon Pokemon { get; set; } = null!;

    /// <summary>Position among the Pokémon's moves, 1-4.</summary>
    public byte Slot { get; set; }

    /// <summary>PokeAPI move slug (e.g. "thunderbolt").</summary>
    [Required]
    [MaxLength(100)]
    public string MoveName { get; set; } = string.Empty;
}
