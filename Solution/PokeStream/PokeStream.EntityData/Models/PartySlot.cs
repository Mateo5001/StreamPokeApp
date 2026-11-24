using System.ComponentModel.DataAnnotations;

namespace PokeStream.EntityData.Models;

/// <summary>One of the 6 main-team slots of a trainer. Rule (service layer): the Pokémon must belong to the trainer.</summary>
public class PartySlot
{
    [Key]
    public int Id { get; set; }

    public Guid TrainerId { get; set; }
    public virtual User Trainer { get; set; } = null!;

    /// <summary>Position in the team, 1-6.</summary>
    public byte Slot { get; set; }

    public Guid PokemonId { get; set; }
    public virtual Pokemon Pokemon { get; set; } = null!;
}
