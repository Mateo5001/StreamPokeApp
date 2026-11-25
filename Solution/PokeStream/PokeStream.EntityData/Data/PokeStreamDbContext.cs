using Microsoft.EntityFrameworkCore;
using PokeStream.EntityData.Models;

namespace PokeStream.EntityData.Data;

/// <summary>
/// Entity Framework DbContext for PokéStream application - Auth and Pokémon modules
/// </summary>
public class PokeStreamDbContext : DbContext
{
    public PokeStreamDbContext(DbContextOptions<PokeStreamDbContext> options) : base(options)
    {
    }

    // DbSets for Auth entities only
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;

    // Chat bots
    public DbSet<BotCredential> BotCredentials { get; set; } = null!;

    // Pokémon module
    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<PokemonMove> PokemonMoves { get; set; } = null!;
    public DbSet<PartySlot> PartySlots { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureUser(modelBuilder);
        ConfigureUserSession(modelBuilder);
        ConfigurePokemon(modelBuilder);
        ConfigurePokemonMove(modelBuilder);
        ConfigurePartySlot(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();

        entity.ToTable("Users");
        entity.HasKey(u => u.Id);
        entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
        entity.Property(u => u.DisplayName).HasMaxLength(200);
        entity.Property(u => u.Email).HasMaxLength(320);
        entity.Property(u => u.AvatarUrl).HasMaxLength(500);
        entity.Property(u => u.TwitchUserId).HasMaxLength(100);
        entity.Property(u => u.TwitchUsername).HasMaxLength(100);
        entity.Property(u => u.YouTubeChannelId).HasMaxLength(100);
        entity.Property(u => u.YouTubeUsername).HasMaxLength(100);
        entity.Property(u => u.KickUserId).HasMaxLength(100);
        entity.Property(u => u.KickUsername).HasMaxLength(100);
        entity.Property(u => u.DiscordUserId).HasMaxLength(100);
        entity.Property(u => u.DiscordUsername).HasMaxLength(100);
        entity.Property(u => u.TwitchAccessToken).HasMaxLength(1000);
        entity.Property(u => u.TwitchRefreshToken).HasMaxLength(1000);
        entity.Property(u => u.YouTubeAccessToken).HasMaxLength(1000);
        entity.Property(u => u.YouTubeRefreshToken).HasMaxLength(1000);
        entity.Property(u => u.KickAccessToken).HasMaxLength(1000);
        entity.Property(u => u.KickRefreshToken).HasMaxLength(1000);

        // Indexes
        entity.HasIndex(u => u.Username).IsUnique();
        entity.HasIndex(u => u.Email).IsUnique();
        entity.HasIndex(u => u.TwitchUserId).IsUnique();
        entity.HasIndex(u => u.YouTubeChannelId).IsUnique();
        entity.HasIndex(u => u.KickUserId).IsUnique();
        entity.HasIndex(u => u.DiscordUserId).IsUnique();
        entity.HasIndex(u => u.IsActive);

        // Relationships
        entity.HasMany(u => u.Sessions)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureUserSession(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<UserSession>();

        entity.ToTable("UserSessions");
        entity.HasKey(s => s.Id);
        entity.Property(s => s.RefreshToken).IsRequired().HasMaxLength(500);
        entity.Property(s => s.DeviceInfo).HasMaxLength(200);
        entity.Property(s => s.IpAddress).HasMaxLength(100);
        entity.Property(s => s.UserAgent).HasMaxLength(500);
        entity.Property(s => s.RevokedReason).HasMaxLength(500);
        entity.Property(s => s.UpdatedAt).IsRequired();

        // Indexes
        entity.HasIndex(s => s.UserId);
        entity.HasIndex(s => s.RefreshToken).IsUnique();
        entity.HasIndex(s => s.ExpiresAt);
        entity.HasIndex(s => s.IsActive);
    }

    private void ConfigurePokemon(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Pokemon>();

        entity.ToTable("Pokemons", t =>
        {
            t.HasCheckConstraint("CK_Pokemons_Level", "[Level] BETWEEN 1 AND 100");
            t.HasCheckConstraint("CK_Pokemons_Ivs",
                "[IvHp] <= 31 AND [IvAttack] <= 31 AND [IvDefense] <= 31 AND [IvSpecialAttack] <= 31 AND [IvSpecialDefense] <= 31 AND [IvSpeed] <= 31");
            t.HasCheckConstraint("CK_Pokemons_Evs",
                "[EvHp] <= 252 AND [EvAttack] <= 252 AND [EvDefense] <= 252 AND [EvSpecialAttack] <= 252 AND [EvSpecialDefense] <= 252 AND [EvSpeed] <= 252 " +
                "AND [EvHp] + [EvAttack] + [EvDefense] + [EvSpecialAttack] + [EvSpecialDefense] + [EvSpeed] <= 510");
        });
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Nickname).HasMaxLength(50);
        entity.Property(p => p.AbilityName).IsRequired().HasMaxLength(100);
        entity.Property(p => p.Nature).HasConversion<string>().HasMaxLength(20);
        entity.Property(p => p.Gender).HasConversion<string>().HasMaxLength(20);
        entity.Property(p => p.CapturedFrom).HasConversion<string>().HasMaxLength(20);
        entity.Property(p => p.RowVersion).IsRowVersion();

        // Indexes
        entity.HasIndex(p => p.TrainerId);
        entity.HasIndex(p => p.SpeciesId);
        entity.HasIndex(p => new { p.TrainerId, p.SpeciesId }); // "which species does this trainer own" (Pokédex)
        entity.HasIndex(p => new { p.OriginStreamerId, p.ExpiresAt }); // active wild Pokémon of a channel

        // Relationships: trainer is released to wild on delete; origin streamer is never cascaded (avoids multiple cascade paths)
        entity.HasOne(p => p.Trainer)
            .WithMany(u => u.Pokemons)
            .HasForeignKey(p => p.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);

        entity.HasOne(p => p.OriginStreamer)
            .WithMany()
            .HasForeignKey(p => p.OriginStreamerId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private void ConfigurePokemonMove(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<PokemonMove>();

        entity.ToTable("PokemonMoves", t => t.HasCheckConstraint("CK_PokemonMoves_Slot", "[Slot] BETWEEN 1 AND 4"));
        entity.HasKey(m => m.Id);
        entity.Property(m => m.MoveName).IsRequired().HasMaxLength(100);

        entity.HasIndex(m => new { m.PokemonId, m.Slot }).IsUnique();
        entity.HasIndex(m => new { m.PokemonId, m.MoveName }).IsUnique();

        entity.HasOne(m => m.Pokemon)
            .WithMany(p => p.Moves)
            .HasForeignKey(m => m.PokemonId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigurePartySlot(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<PartySlot>();

        entity.ToTable("PartySlots", t => t.HasCheckConstraint("CK_PartySlots_Slot", "[Slot] BETWEEN 1 AND 6"));
        entity.HasKey(s => s.Id);

        entity.HasIndex(s => new { s.TrainerId, s.Slot }).IsUnique(); // one Pokémon per slot
        entity.HasIndex(s => s.PokemonId).IsUnique();                 // a Pokémon sits in one slot only

        entity.HasOne(s => s.Trainer)
            .WithMany(u => u.Party)
            .HasForeignKey(s => s.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(s => s.Pokemon)
            .WithMany()
            .HasForeignKey(s => s.PokemonId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
