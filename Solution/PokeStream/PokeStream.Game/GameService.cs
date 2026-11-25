using Microsoft.EntityFrameworkCore;
using PokeApiNet;
using PokeStream.EntityData.Data;
using PokeStream.EntityData.Models;
using Nature = PokeStream.EntityData.Models.Nature;

namespace PokeStream.Game;

/// <summary>A Pokémon that left the wild: captured or fled.</summary>
public record Departed(Guid StreamerId, string SpeciesName);

/// <summary>Who typed the capture command, as the platform knows them.</summary>
public record Viewer(string PlatformUserId, string Login, string DisplayName);

public record TeamPokemon(string Name, int Level, bool IsShiny, string? SpriteUrl);

/// <summary>What a trainer owns.</summary>
public record Collection(IReadOnlyList<int> SpeciesIds, IReadOnlyList<TeamPokemon> Team);

/// <summary>Spawn, capture and flee rules. Knows nothing about chat.</summary>
public class GameService
{
    public static readonly TimeSpan CaptureWindow = TimeSpan.FromSeconds(90);

    private const int TeamSize = 6;
    private const int FirstGeneration = 151;
    private const int MaxSpawnLevel = 30;
    private const int ShinyOdds = 4096;

    private readonly PokeStreamDbContext _db;
    private readonly PokeApiClient _pokeApi;
    private readonly IGameEvents _events;

    public GameService(PokeStreamDbContext db, PokeApiClient pokeApi, IGameEvents events)
    {
        _db = db;
        _pokeApi = pokeApi;
        _events = events;
    }

    /// <summary>
    /// Makes a wild Pokémon appear in the channel. Null when one is already there.
    /// Callers must not run two spawns of the same channel at once.
    /// </summary>
    public async Task<WildPokemon?> SpawnAsync(Guid streamerId, CancellationToken cancellationToken = default)
    {
        if (await Wild(streamerId).AnyAsync(cancellationToken))
            return null;

        var pokemon = await _pokeApi.GetResourceAsync<PokeApiNet.Pokemon>(Random.Shared.Next(1, FirstGeneration + 1), cancellationToken);
        var species = await _pokeApi.GetResourceAsync(pokemon.Species, cancellationToken);
        var abilities = pokemon.Abilities.Where(a => !a.IsHidden).ToList();

        var wild = new EntityData.Models.Pokemon
        {
            SpeciesId = species.Id,
            Level = (byte)Random.Shared.Next(1, MaxSpawnLevel + 1),
            Nature = (Nature)Random.Shared.Next(Enum.GetValues<Nature>().Length),
            Gender = RandomGender(species.GenderRate),
            IsShiny = Random.Shared.Next(ShinyOdds) == 0,
            AbilityName = abilities[Random.Shared.Next(abilities.Count)].Ability.Name,
            IvHp = RandomIv(),
            IvAttack = RandomIv(),
            IvDefense = RandomIv(),
            IvSpecialAttack = RandomIv(),
            IvSpecialDefense = RandomIv(),
            IvSpeed = RandomIv(),
            OriginStreamerId = streamerId,
            SpawnedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow + CaptureWindow
        };
        _db.Pokemons.Add(wild);
        await _db.SaveChangesAsync(cancellationToken);

        var appeared = Describe(wild, pokemon, species);
        await _events.SpawnedAsync(streamerId, appeared);
        return appeared;
    }

    /// <summary>
    /// Gives the channel's wild Pokémon to the viewer, creating a provisional account for a new one.
    /// Null when there is nothing to capture or somebody else was faster.
    /// </summary>
    public async Task<Departed?> CaptureAsync(Guid streamerId, Viewer viewer, CancellationToken cancellationToken = default)
    {
        var wild = await Wild(streamerId).FirstOrDefaultAsync(cancellationToken);
        if (wild is null)
            return null;

        var trainer = await _db.Users.FirstOrDefaultAsync(u => u.TwitchUserId == viewer.PlatformUserId, cancellationToken);
        if (trainer is { IsActive: false })
            return null;

        // Resolved before saving: a failure here must not leave a capture that nobody was told about
        var name = await SpeciesNameAsync(wild.SpeciesId, cancellationToken);

        if (trainer is null)
        {
            trainer = new User
            {
                Username = User.ProvisionalPrefix + viewer.PlatformUserId,
                DisplayName = viewer.DisplayName,
                TwitchUserId = viewer.PlatformUserId,
                TwitchUsername = viewer.Login,
                IsProvisional = true
            };
            _db.Users.Add(trainer);
        }

        wild.Trainer = trainer;
        wild.CapturedAt = DateTime.UtcNow;
        wild.CapturedFrom = CapturePlatform.Twitch;
        wild.ExpiresAt = null;
        try
        {
            // One transaction: losing the race also discards the new account
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException) // includes the row version conflict
        {
            _db.ChangeTracker.Clear();
            return null;
        }
        await _events.CapturedAsync(streamerId, name, viewer.DisplayName);
        return new Departed(streamerId, name);
    }

    /// <summary>Removes the wild Pokémon nobody captured in time and reports them.</summary>
    public async Task<IReadOnlyList<Departed>> FleeExpiredAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var expired = await _db.Pokemons
            .Where(p => p.TrainerId == null && p.CapturedAt == null && p.ExpiresAt <= now && p.OriginStreamerId != null)
            .ToListAsync(cancellationToken);

        var fled = new List<Departed>();
        foreach (var pokemon in expired)
        {
            var name = await SpeciesNameAsync(pokemon.SpeciesId, cancellationToken);
            _db.Pokemons.Remove(pokemon);
            try { await _db.SaveChangesAsync(cancellationToken); }
            catch (DbUpdateConcurrencyException) // captured at the last moment
            {
                _db.ChangeTracker.Clear();
                continue;
            }
            await _events.FledAsync(pokemon.OriginStreamerId!.Value, name);
            fled.Add(new Departed(pokemon.OriginStreamerId!.Value, name));
        }
        return fled;
    }

    /// <summary>The wild Pokémon in the channel right now, or null.</summary>
    public async Task<WildPokemon?> GetActiveAsync(Guid streamerId, CancellationToken cancellationToken = default)
    {
        var wild = await Wild(streamerId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (wild is null)
            return null;

        var pokemon = await _pokeApi.GetResourceAsync<PokeApiNet.Pokemon>(wild.SpeciesId, cancellationToken);
        return Describe(wild, pokemon, await _pokeApi.GetResourceAsync(pokemon.Species, cancellationToken));
    }

    /// <summary>Species the trainer owns, and their team.</summary>
    public async Task<Collection> GetCollectionAsync(Guid trainerId, CancellationToken cancellationToken = default)
    {
        var owned = _db.Pokemons.AsNoTracking().Where(p => p.TrainerId == trainerId);
        var species = await owned.Select(p => p.SpeciesId).Distinct().OrderBy(id => id).ToListAsync(cancellationToken);

        // Nobody edits teams yet, so the team is the first Pokémon captured
        var first = await owned.OrderBy(p => p.CapturedAt).ThenBy(p => p.Id).Take(TeamSize).ToListAsync(cancellationToken);
        var team = new List<TeamPokemon>();
        foreach (var member in first)
        {
            var pokemon = await _pokeApi.GetResourceAsync<PokeApiNet.Pokemon>(member.SpeciesId, cancellationToken);
            var name = member.Nickname ?? await SpeciesNameAsync(member.SpeciesId, cancellationToken);
            team.Add(new TeamPokemon(name, member.Level, member.IsShiny, Sprite(pokemon, member.IsShiny)));
        }
        return new Collection(species, team);
    }

    private static string? Sprite(PokeApiNet.Pokemon pokemon, bool shiny)
        => shiny ? pokemon.Sprites?.FrontShiny : pokemon.Sprites?.FrontDefault;

    private static WildPokemon Describe(EntityData.Models.Pokemon wild, PokeApiNet.Pokemon pokemon, PokemonSpecies species)
        => new(DisplayName(species), wild.Level, wild.IsShiny,
            Sprite(pokemon, wild.IsShiny),
            DateTime.SpecifyKind(wild.ExpiresAt!.Value, DateTimeKind.Utc));

    private IQueryable<EntityData.Models.Pokemon> Wild(Guid streamerId)
    {
        var now = DateTime.UtcNow;
        return _db.Pokemons.Where(p => p.OriginStreamerId == streamerId && p.TrainerId == null && p.CapturedAt == null && p.ExpiresAt > now);
    }

    private async Task<string> SpeciesNameAsync(int speciesId, CancellationToken cancellationToken)
        => DisplayName(await _pokeApi.GetResourceAsync<PokemonSpecies>(speciesId, cancellationToken));

    private static string DisplayName(PokemonSpecies species)
        => species.Names.FirstOrDefault(n => n.Language.Name == "es")?.Name ?? species.Name;

    private static byte RandomIv() => (byte)Random.Shared.Next(32);

    /// <summary>PokeAPI gives the chance of being female in eighths, or -1 for genderless.</summary>
    private static PokemonGender RandomGender(int femaleEighths)
    {
        if (femaleEighths < 0)
            return PokemonGender.Genderless;
        return Random.Shared.Next(8) < femaleEighths ? PokemonGender.Female : PokemonGender.Male;
    }
}
